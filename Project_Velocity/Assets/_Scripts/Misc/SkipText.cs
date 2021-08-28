using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipText : MonoBehaviour
{
    public GameObject skipText;

    public GameObject player;

    public MenuManager enableArm;

    public void  OnClickSkip()
    {
        skipText.SetActive(false);

        player.SetActive(true);

        enableArm.meleePauseDisable.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
