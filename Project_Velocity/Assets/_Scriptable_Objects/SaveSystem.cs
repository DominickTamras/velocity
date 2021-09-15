using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem 
{
    public LevelDataRecorder dataRecord;

    private static readonly string SAVE_FOLDER = Application.dataPath + "/LevelSavedDataRecorded/";


    void Awake()
    {
        if(!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }
    public void LevelSavedData()
    {
        int savedDeaths = dataRecord.currDeaths;

        bool savedCompleted = dataRecord.levelData.isCompleted;

        float savedMinutes = dataRecord.currMinutes;

        float savedSeconds = dataRecord.currSeconds;

        int savedEnemyKill = dataRecord.enemiesKilled;

        LevelDataRecorder savedData = new LevelDataRecorder
        {
            currDeaths = savedDeaths, currMinutes = savedMinutes, currSeconds = savedSeconds, enemiesKilled = savedEnemyKill, currComplete = savedCompleted
        };

        string jsonSave = JsonUtility.ToJson(savedData);

        File.WriteAllText(SAVE_FOLDER + "/LevelSaveData/Level.txt", jsonSave);
    }

    public void LevelLoadData()
    {
        if(File.Exists(SAVE_FOLDER + "/LevelSaveData/Level.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/LevelSaveData/Level.txt");

            LevelDataRecorder SavedData = JsonUtility.FromJson<LevelDataRecorder>(saveString);

            dataRecord.currDeaths = SavedData.currDeaths;

            //https://www.youtube.com/watch?v=6uMFEM-napE&t=64s Go to 6:00 Min, make multiple files per scriptable saved object

        }
    }



}
