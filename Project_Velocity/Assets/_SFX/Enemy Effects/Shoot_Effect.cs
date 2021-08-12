using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_Effect : MonoBehaviour
{


    public AudioSource shootPistol;


  
    void OnEnable()
    {
        shootPistol.Play();

    }



/*    void RandomSound()
    {
        randomSound.clip = footstep[Random.Range(0, footstep.Length)];

        randomSound.Play();

        Debug.Log("PlayedFoot");
    }*/

 

    
}
