using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataRecorder : MonoBehaviour
{
    public LevelDataSO levelData;

    //Timer
    private float timer = 0f;
    private float minutes;
    private float seconds;

    void Update()
    {
        timerCounter();
    }

    private void timerCounter()
    {
        timer = Time.time;
        minutes = ((int) timer / 60);
        seconds = (timer % 60);
    }

    //Still need to add functionality where it only records the data to the SO if it is a highs score
    public void timerRecorder()
    {
        levelData.minutes = minutes;
        levelData.seconds = seconds;
    }
}
