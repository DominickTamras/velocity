using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    //PLACE THIS SCRIPT ON END OF LEVEL COLLIDER
    //REFERENCE THE LevelDataRecorder SCRIPT

    public LevelDataRecorder dataRecorder;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if(player != null)
        {
            dataRecorder.dataRecorder();
        }
    }
}
