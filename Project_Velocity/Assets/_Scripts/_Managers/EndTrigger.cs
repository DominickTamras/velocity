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

    public TextMeshProUGUI kills;

    public TextMeshProUGUI mins;

    public TextMeshProUGUI secs;



    public void Update()
    {
        kills.text = dataRecorder.enemiesKilled.ToString();

        mins.text = dataRecorder.currMinutes.ToString();

        secs.text = dataRecorder.currSeconds.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if(player != null)
        {
            scoreScreen.SetActive(true);
            dataRecorder.dataRecorder();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
