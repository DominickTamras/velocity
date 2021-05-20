using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro_Range : MonoBehaviour
{
    EnemyTracking check;

    private void Awake()
    {
        check = FindObjectOfType<EnemyTracking>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            //check.target = other.gameObject;
            check.isOut = false;
            check.isIn = true;

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //check.target = null;
            check.finalTime = 0;
            check.isIn = false;
            check.isOut = true;
         
        }
    }


}
