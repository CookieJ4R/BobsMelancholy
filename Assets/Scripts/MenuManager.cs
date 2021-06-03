using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{

    public GameObject standardSelect;

    public GameObject playPanel;

    public GameObject records;

    public GameObject options;

    public AudioMenuManager audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Cursor.visible = false;
        EventSystem.current.SetSelectedGameObject(standardSelect);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Back") && GUIOpen())
        {
            audioSource.PlaySound(1);
            playPanel.SetActive(false);
            records.SetActive(false);
            options.SetActive(false);
            EventSystem.current.SetSelectedGameObject(standardSelect);
        }

    }

    bool GUIOpen()
    {
        return playPanel.activeSelf || records.activeSelf || options.activeSelf;
    }


    public void StartGame(int level)
    {
        audioSource.PlaySound(1);
        if (EventSystem.current.currentSelectedGameObject.name.Equals("Full Run"))
            RunOptions.fullRun = true;
        else
            RunOptions.fullRun = false;
        RunOptions.level = level;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void Play()
    {
        audioSource.PlaySound(1);
        playPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(playPanel.transform.GetChild(0).gameObject);
    }

    public void Records()
    {
        EventSystem.current.SetSelectedGameObject(null);
        audioSource.PlaySound(1);
        SetUpTable();
    }

    public void Options()
    {
        audioSource.PlaySound(1);
        options.SetActive(true);
        EventSystem.current.SetSelectedGameObject(options.transform.GetChild(1).GetChild(0).gameObject);
    }


    string FormatTime(float timeIn)
    {
        float totalSeconds = timeIn;
        TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
        return time.ToString("hh':'mm':'ss'.'ff");
    }

    void SetUpTable()
    {
        Transform valueParent = records.transform.GetChild(1).GetChild(2);

        for(int i = 0; i < 6; i++)
        {
            int deaths;
            string s;

            if (i == 0)
                deaths = PlayerPrefs.GetInt("DeathsGame", -1);
            else
                deaths = PlayerPrefs.GetInt("DeathsLVL" + i, -1);
            if (deaths == -1)
                s = "---";
            else
                s = deaths.ToString();

            valueParent.GetChild(i).GetComponent<TMPro.TMP_Text>().text = s;
        }

        for(int i = 0; i < 6; i++)
        {
            float time;
            string s;

            if (i == 0)
                time = PlayerPrefs.GetFloat("TimeGame", -1);
            else
                time = PlayerPrefs.GetFloat("TimeLVL" + i, -1);

            if (time == -1)
                s = "---";
            else
                s = FormatTime(time);

            valueParent.GetChild(6 + i).GetComponent<TMPro.TMP_Text>().text = s;

        }

        records.SetActive(true);


    }

    public void Quit()
    {
        audioSource.PlaySound(1);
        Application.Quit();
    }

}
