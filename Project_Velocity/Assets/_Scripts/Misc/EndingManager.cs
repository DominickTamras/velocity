using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;

public class EndingManager : MonoBehaviour
{
    public Animator theMadam;

    public Animator player;

    public Light lightIncrease;

    public GameObject core;

    public Camera cameraDie;

    public GameObject playerDie;

    public GameObject sun;

    public MMFeedbacks feedbackStart;



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
      
            feedbackStart.PlayFeedbacks();

            theMadam.SetTrigger("StartDeathTrigger");
           
            lightIncrease.intensity = Mathf.Lerp(lightIncrease.intensity, 10000000, Time.deltaTime / 10000000 * increasedTime );

            sun.transform.localScale = Vector3.Lerp(sun.transform.localScale, sun.transform.localScale * 10, Time.deltaTime / 90000 * increasedTime);
            
            yield return new WaitForSeconds(7);

            cameraDie.enabled = false;

            playerDie.SetActive(false);

            player.Play("GlitchEffect");

            yield return new WaitForSeconds(3);

            SceneManager.LoadScene("Credits");
        }

      

        // Trigger animations on objects destruction.
        // Include 'dying' animation look. Head tilt down or something.
        //Shake camera or something
        //Trigger glitch effect
        //Transition into credits
    }



}
