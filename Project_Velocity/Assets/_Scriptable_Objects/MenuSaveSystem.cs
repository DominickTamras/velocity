using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSaveSystem : MonoBehaviour
{
    public LevelDataSO[] levelDatas = new LevelDataSO[9]; //Level 1 will be stored in 0, do +1
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelLoadData();
        }
    }

    public void LevelLoadData()
    {
        foreach (LevelDataSO SO in levelDatas)
        {

            string saveString = SaveManager.Loading(SO.iD);

            Debug.Log(saveString);
            if (saveString != null) //CHECKS IF FILE EXISTS
            {


                //LevelDataRecorder SavedData = JsonUtility.FromJson<LevelDataRecorder>(saveString); CANT USE FROMJSON WITH SOs
                LevelDataSO SavedData = new LevelDataSO();
                JsonUtility.FromJsonOverwrite(saveString, SavedData);
                levelDatas[int.Parse(SO.iD) - 1].isCompleted = SavedData.isCompleted;
                levelDatas[int.Parse(SO.iD) - 1].iD = SavedData.iD;
                levelDatas[int.Parse(SO.iD) - 1].minutes = SavedData.minutes;
                levelDatas[int.Parse(SO.iD) - 1].seconds = SavedData.seconds;
                levelDatas[int.Parse(SO.iD) - 1].totalEnemies = SavedData.totalEnemies;
                levelDatas[int.Parse(SO.iD) - 1].enemiesKilled = SavedData.enemiesKilled;
                levelDatas[int.Parse(SO.iD) - 1].deaths = SavedData.deaths; // Put .whaetver thats on the SO like .deaths, .kills, etc. 



                //https://www.youtube.com/watch?v=6uMFEM-napE&t=64s Go to 6:00 Min, make multiple files per scriptable saved object

            }
        }
    }
}
