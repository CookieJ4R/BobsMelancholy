using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IngameMenu : MonoBehaviour
{

    public GameObject menu;

    public GameObject options;

    public GameObject defaultSelection;

    public AudioManager audioManager;

    LevelManager lm;

    // Start is called before the first frame update
    void Start()
    {
        lm = GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Back"))
        {
            if (options.activeSelf)
            {
                audioManager.PlaySound(1);
                options.SetActive(false);
               EventSystem.current.SetSelectedGameObject(defaultSelection);
            }
            else if (menu.activeSelf)
            {
                audioManager.PlaySound(1);
                CloseMenu();
            }
                
        }
            

        if (Input.GetButtonDown("Menu"))
        {
            if (menu.activeSelf && options.activeSelf == false)
            {
                audioManager.PlaySound(1);
                CloseMenu();
            }
            else if(!menu.activeSelf && !lm.GameFinishOverview.activeSelf && !lm.LevelFinishedOverview.activeSelf)
            {
                audioManager.PlaySound(1);
                menu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(defaultSelection);
                lm.canRespawn = false;
                lm.lsm.StopLevelTimer();
                lm.lsm.StopGameTimer();
                try
                {
                    Player player = GameObject.Find("Player").GetComponent<Player>();
                    player.ToggleCanMoveNextFrame();
                }
                catch (Exception e)
                {
                    print(e.StackTrace);
                }
            }
        }


    }

    public void Options()
    {
        audioManager.PlaySound(1);
        options.SetActive(true);
        EventSystem.current.SetSelectedGameObject(options.transform.GetChild(1).GetChild(0).gameObject);
    }

    public void Resume()
    {
        audioManager.PlaySound(1);
        CloseMenu();
    }

    public void MainMenu()
    {
        audioManager.PlaySound(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    void CloseMenu()
    {
        defaultSelection.GetComponent<PlaySound>().enabled = false;
        menu.SetActive(false);
        lm.canRespawn = true;
        lm.lsm.ResumeLevelTimer();
        lm.lsm.ResumeGameTimer();
        try
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            player.ToggleCanMoveNextFrame();
        }
        catch (Exception e)
        {
            print(e.StackTrace);
        }        
    }


}
