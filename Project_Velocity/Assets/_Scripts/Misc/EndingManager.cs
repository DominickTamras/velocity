using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public Animator theMadam;

    public Animator player;

    public Light lightIncrease;

    public GameObject core;





    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
 
        StartCoroutine(BeginEnd());
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            theMadam.SetTrigger("StartCoreOpen");
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }




    IEnumerator BeginEnd()
    {
        float increasedTime = 90;

        if (core == null)
        {   
            
            theMadam.Play("Death");
           
            lightIncrease.intensity = Mathf.Lerp(lightIncrease.intensity, 10000000, Time.deltaTime / 10000000 * increasedTime );
            
            yield return new WaitForSeconds(15);

            player.Play("GlitchEffect");
        }

      

        // Trigger animations on objects destruction.
        // Include 'dying' animation look. Head tilt down or something.
        //Shake camera or something
        //Trigger glitch effect
        //Transition into credits
    }



}
