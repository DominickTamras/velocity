using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipText : MonoBehaviour
{
    public GameObject skipText;

    public GameObject player;

    public void  OnClickSkip()
    {
        skipText.SetActive(false);

        player.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
