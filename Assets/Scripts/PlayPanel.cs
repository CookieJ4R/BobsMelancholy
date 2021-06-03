using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanel : MonoBehaviour
{

    public Button[] levels;

    // Start is called before the first frame update
    void Start()
    {
        int unlockedLevels = PlayerPrefs.GetInt("Levels", 0);

        for(int i = 0; i < levels.Length; i++)
        {
            if (i < unlockedLevels)
                continue;
            levels[i].interactable = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
