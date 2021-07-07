using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Level_Data")]
public class LevelDataSO : ScriptableObject
{
    public bool isCompleted;

    //Time Score
    public float minutes;
    public float seconds;

    //Enemy Score
    public int totalEnemies;
    public int enemiesKilled;
}
