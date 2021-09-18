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


                LevelDataRecorder SavedData = JsonUtility.FromJson<LevelDataRecorder>(saveString);
                levelDatas[int.Parse(SO.iD)-1].deaths = SavedData.levelData.deaths; // Put .whaetver thats on the SO like .deaths, .kills, etc. 
                Debug.Log(SavedData.levelData.deaths);


                //dataRecord.currDeaths = SavedData.currDeaths;


                //load other stuff when done testing

                //https://www.youtube.com/watch?v=6uMFEM-napE&t=64s Go to 6:00 Min, make multiple files per scriptable saved object

            }
        }
    }
}
