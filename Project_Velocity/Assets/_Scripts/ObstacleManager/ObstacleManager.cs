using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager current;
    private void Awake()
    {
        current = this;
    }

    // Update is called once per frame
    public event Action onObstacleTrigger;
    public void ObstacleTriggerEnter()
    {
        if(onObstacleTrigger != null)
        {
            onObstacleTrigger();
        }
    }
}
