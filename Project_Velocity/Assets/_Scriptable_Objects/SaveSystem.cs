using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public LevelDataRecorder dataRecord;

    void Awake()
    {
        SaveManager.InIt();
       
    }

    private void Update()
    {
       /* if(Input.GetKeyDown(KeyCode.S))
        {
            LevelSavedData();
        }*/

        /*if (Input.GetKeyDown(KeyCode.L))
        {
            LevelLoadData();
        }*/
    }

    public void LevelSavedData() // THIS RECORDS THE DATA FROM THE SCRIPTABLE INNTO SAVE
    {
        /*int savedDeaths = dataRecord.currDeaths;

        bool savedCompleted = dataRecord.levelData.isCompleted;

        float savedMinutes = dataRecord.currMinutes;

        float savedSeconds = dataRecord.currSeconds;

        int savedEnemyKill = dataRecord.enemiesKilled;

        string idCheck = dataRecord.levelData.iD;

        LevelDataRecorder savedData = new LevelDataRecorder // ASSIGNS THE DATA HERE
        {
            levelData = dataRecord.levelData, currDeaths = savedDeaths, currMinutes = savedMinutes, currSeconds = savedSeconds, enemiesKilled = savedEnemyKill, currComplete = savedCompleted
           
        };

        string jsonSave = JsonUtility.ToJson(savedData);*/
        string jsonSave = JsonUtility.ToJson(dataRecord.levelData);
        Debug.Log("Saved");

        SaveManager.Saving(jsonSave); // SAVES TO JSON IN OTHER SCRIPT
    }

    /*public void LevelLoadData()
    {
        string saveString = SaveManager.Loading();
        if (saveString != null) //CHECKS IF FILE EXISTS
        {

            LevelDataRecorder SavedData = JsonUtility.FromJson<LevelDataRecorder>(saveString);

            dataRecord.currDeaths = SavedData.currDeaths;


            //load other stuff when done testing

            //https://www.youtube.com/watch?v=6uMFEM-napE&t=64s Go to 6:00 Min, make multiple files per scriptable saved object

        }
    }*/



}
