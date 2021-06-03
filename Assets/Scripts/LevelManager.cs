using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public Vector3 respawnPoint;

    GameObject player;

    public int curLevelID = 1;

    public GameObject[] level;

    public GameObject playerPrefab;

    public GameObject LevelFinishedOverview;
    public GameObject GameFinishOverview;

    Dictionary<int, Vector3> spawnpoints = new Dictionary<int, Vector3>();

    public LevelStatsManager lsm;

    public bool canRespawn = true;

    private void Awake()
    {
        player = GameObject.Find("Player");
        lsm = GetComponent<LevelStatsManager>();
        setUpSpawnpoints();
    }

    private void Start()
    {
        curLevelID = RunOptions.level;
        respawnPoint = spawnpoints[curLevelID];
        SetUpLevelAndPlayer(curLevelID);
        lsm.StartGameTimer();
        lsm.StartLevelTimer();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Reset"))
            ReloadLevel();

    }

    void setUpSpawnpoints()
    {
        spawnpoints.Add(1, new Vector3(-20, -9, 0));
        spawnpoints.Add(2, new Vector3(-20, -7, 0));
        spawnpoints.Add(3, new Vector3(-19.5f, -9, 0));
        spawnpoints.Add(4, new Vector3(-20, -8.5f, 0));
        spawnpoints.Add(5, new Vector3(-20, 0.5f, 0));
    }

    public void FinishLevel()
    {
        canRespawn = false;
        Destroy(GameObject.Find("Player"));
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayGoalSound();
        lsm.StopLevelTimer();
        int deaths = lsm.GetLevelDeathCount();
        lsm.AddGameDeath(deaths);
        int highscoreDeaths = PlayerPrefs.GetInt("DeathsLVL" + curLevelID, -1);

        if (deaths < highscoreDeaths || highscoreDeaths == -1)
        {
            PlayerPrefs.SetInt("DeathsLVL" + curLevelID, deaths);
            LevelFinishedOverview.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        }
        else
            LevelFinishedOverview.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);

        float time = lsm.GetLevelTimer();
        float highscoreTime = PlayerPrefs.GetFloat("TimeLVL" + curLevelID, -1);
        if (time < highscoreTime || highscoreTime == -1)
        {
            PlayerPrefs.SetFloat("TimeLVL" + curLevelID, time);
            LevelFinishedOverview.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        }
        else
            LevelFinishedOverview.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);


        LevelFinishedOverview.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = "Level " + curLevelID;
        LevelFinishedOverview.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = "Deaths: "+  Environment.NewLine + lsm.GetLevelDeathCount();
        LevelFinishedOverview.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = "Time: " + Environment.NewLine + FormatTime(time);
        LevelFinishedOverview.SetActive(true);

    }

    string FormatTime(float timeIn)
    {
        float totalSeconds = timeIn;
        TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
        return time.ToString("hh':'mm':'ss'.'ff");
    }

    public void LoadNextLevel()
    {
        if (!RunOptions.fullRun)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            return;
        }
            
        if (curLevelID < level.Length)
        {
            Destroy(GameObject.Find("Level " + curLevelID));
            LevelFinishedOverview.SetActive(false);
            lsm.StartLevelTimer();
            lsm.ResetLevelDeathCount();
            curLevelID++;           
            respawnPoint = spawnpoints[curLevelID];
            SetUpLevelAndPlayer(curLevelID);
            canRespawn = true;
        }
        else
        {
            LevelFinishedOverview.SetActive(false);
            ShowGameSummary();
            // Show game end screen with gametimer and total deathcount
        }
    }

    void ReloadLevel()
    {
        if (canRespawn)
        {
            Destroy(GameObject.Find("Level " + curLevelID));
            if (player != null)
                Destroy(GameObject.Find("Player"));
            Instantiate(level[curLevelID - 1], Vector2.zero, Quaternion.identity).name = "Level " + curLevelID;
            player = Instantiate(playerPrefab, respawnPoint, Quaternion.identity);
            player.name = "Player";
        }
    }

    void SetUpLevelAndPlayer(int levelID)
    {
        int unlockedLevels = PlayerPrefs.GetInt("Levels", 0);

        if (levelID > unlockedLevels)
            PlayerPrefs.SetInt("Levels", levelID);

        Instantiate(level[levelID - 1], Vector2.zero, Quaternion.identity).name = "Level " + levelID;
        player = Instantiate(playerPrefab, respawnPoint, Quaternion.identity);
        player.name = "Player";
    }


    void ShowGameSummary()
    {
        lsm.StopGameTimer();
        int deaths = lsm.GetGameDeathCount();
        int highscoreDeaths = PlayerPrefs.GetInt("DeathsGame", -1);

        if (deaths < highscoreDeaths || highscoreDeaths == -1)
        {
            PlayerPrefs.SetInt("DeathsGame", deaths);
            GameFinishOverview.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        }
        else
            GameFinishOverview.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);

        float time = lsm.GetGameTimer();
        float highscoreTime = PlayerPrefs.GetFloat("TimeGame", -1);
        if (time < highscoreTime || highscoreTime == -1)
        {
            PlayerPrefs.SetFloat("TimeGame", time);
            GameFinishOverview.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        }
        else
            GameFinishOverview.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);


        GameFinishOverview.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = "Deaths: " + Environment.NewLine + deaths;
        GameFinishOverview.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = "Time: " + Environment.NewLine + FormatTime(time);
        GameFinishOverview.SetActive(true);
    }


}
