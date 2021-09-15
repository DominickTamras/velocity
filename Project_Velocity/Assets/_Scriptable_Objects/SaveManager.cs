using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager

{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/LevelSavedDataRecorded/";
    public static void InIt()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Saving (string saveString)
    {
        

        int saveNumber = 1;

        while(File.Exists("levelData_" + saveNumber + ".txt"))
       {
            saveNumber++;
       }

        File.WriteAllText(SAVE_FOLDER + "/LevelDataSave" + saveNumber + ".txt", saveString);

    }

    public static string Loading()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER); // Adds directory info and creates a folder
        FileInfo[] saveFiles = directoryInfo.GetFiles(".txt"); // GRABS SAVED FILES AND PUTS INTO ARRAY
        FileInfo recentFile = null;
        foreach(FileInfo fileInfo in saveFiles)
        {
            if (recentFile == null)
            {
                recentFile = fileInfo;
            }
            else
            {
                if (fileInfo.LastWriteTime > recentFile.LastWriteTime)
                {
                    recentFile = fileInfo;
                }
            }
        }

        if (recentFile != null)
        {
            string saveString = File.ReadAllText(recentFile.FullName);
            return saveString;
        }

        else
        {
            return null;
        }


       
    }

    // Works but now we need to uniquely assign each file to the appropriate Scriptable Object

}
