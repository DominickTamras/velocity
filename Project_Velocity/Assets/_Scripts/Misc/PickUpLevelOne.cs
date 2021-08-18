using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLevelOne : MonoBehaviour
{
    // Disable Shooting and Right arm in level 1
    public GameObject enableArm;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            enableArm.SetActive(true);
            other.GetComponent<Shooting>().enabled = true;
            this.gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().PlaySound("Equip");

        }
    }


}
