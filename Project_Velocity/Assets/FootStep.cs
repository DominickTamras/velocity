using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    public AudioClip[] footstep;

    public AudioSource randomSound;

    public int swapNum;


  
    void OnEnable()
    {
        randomSound = GetComponent<AudioSource>();

        if (swapNum == 0)
        {
            randomSound.clip = footstep[0];

          

        }

        else if (swapNum == 1)
        {
            randomSound.clip = footstep[1];
 

        }

        randomSound.Play();

        /* int randomClip = Random.Range(0, footstep.Length);*/


    }



/*    void RandomSound()
    {
        randomSound.clip = footstep[Random.Range(0, footstep.Length)];

        randomSound.Play();

        Debug.Log("PlayedFoot");
    }*/

 

    
}
