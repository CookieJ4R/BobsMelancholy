using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishedScreen : MonoBehaviour
{

    LevelManager lm;

    public bool gameEndScreen;

    private void Start()
    {
        lm = GameObject.Find("Manager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!gameEndScreen)
                lm.LoadNextLevel();
            else
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}
