using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
   List<MonoBehaviour> allScripts = new List<MonoBehaviour>();//Something to disable player script
   
    void Awake()
    {
        //Get to disable player at start.
    }

    // Update is called once per frame
    void Update()
    {
        //Play aniamtion
    }

    private void OnTriggerEnter(Collider other)
    {


        if(other.tag == "Player")
        {
            
            allScripts.Add(other.GetComponent<MonoBehaviour>());



        }
    }

}
