using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndTrigger : MonoBehaviour
{
    //PLACE THIS SCRIPT ON END OF LEVEL COLLIDER
    //REFERENCE THE LevelDataRecorder SCRIPT

    public LevelDataRecorder dataRecorder;

    public GameObject scoreScreen;

    public GameObject playerStop;

    public Animator footStepStop;

    public MeleeAttack meleeDisableEnd;

    public Animator playAnim2;

    public TextMeshProUGUI kills;

    public TextMeshProUGUI mins;

    public TextMeshProUGUI secs;
    
    private bool endReached;



    public void Update()
    {
        kills.text = dataRecorder.enemiesKilled.ToString();

        mins.text = dataRecorder.currMinutes.ToString();

        secs.text = dataRecorder.currSeconds.ToString();

        if(endReached == true)
        {
            FindObjectOfType<AudioManager>().StopSound("Slide");
            footStepStop.enabled = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if(player != null)
        {
            scoreScreen.SetActive(true);
            FindObjectOfType<AudioManager>().PlaySound("Player_Endlevel1");
            FindObjectOfType<AudioManager>().PlaySound("Player_Endlevel2");
            FindObjectOfType<PlayerGoByeBye>().main_Camera.SetActive(false);
            playAnim2.Play("GlitchEffect");
            playerStop.SetActive(false);
            meleeDisableEnd.enabled = false;
            dataRecorder.dataRecorder();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            endReached = true;
            

          
        }
    }
}
