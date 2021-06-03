using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatsManager : MonoBehaviour
{

    private int leveldeathCount = 0;
    private int gamedeathCount = 0;
    private float levelTimer;
    private float gameTimer;

    bool levelTimerActive = false;

    bool gameTimerActive = false;

    public void AddLevelDeath()
    {
        leveldeathCount++;
    }

    public void ResetLevelDeathCount()
    {
        leveldeathCount = 0;
    }

    public int GetLevelDeathCount()
    {
        return leveldeathCount;
    }

    public void AddGameDeath(int levelDeaths)
    {
        gamedeathCount += levelDeaths;
    }

    public void ResetGameDeathCount()
    {
        leveldeathCount = 0;
    }

    public int GetGameDeathCount()
    {
        return gamedeathCount;
    }

    public void StartLevelTimer()
    {
        levelTimerActive = true;
        levelTimer = 0;
    }

    public void StopLevelTimer()
    {
        levelTimerActive = false;
    }

    public void ResumeLevelTimer()
    {
        levelTimerActive = true;
    }

    public float GetLevelTimer()
    {
        return Truncate(levelTimer, 3);
    }

    public void StartGameTimer()
    {
        gameTimerActive = true;
        gameTimer = 0;
    }

    public void StopGameTimer()
    {
        gameTimerActive = false;
    }

    public void ResumeGameTimer()
    {
        gameTimerActive = true;
    }

    public float GetGameTimer()
    {
        return Truncate(gameTimer, 3);
    }

    private void Update()
    {
        if (levelTimerActive)
            levelTimer += Time.deltaTime;
        if (gameTimerActive)
            gameTimer += Time.deltaTime;
    }


    float Truncate(float value, int digits)
    {
        double mult = Math.Pow(10.0, digits);
        double result = Math.Truncate(mult * value) / mult;
        return (float)result;
    }


}
