using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class Achievement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SteamUserStats.SetAchievement("BLOW_UP_THE_SUN");
        SteamUserStats.StoreStats();
    }

    
}
