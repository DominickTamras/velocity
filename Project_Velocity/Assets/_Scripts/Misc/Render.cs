using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Render : MonoBehaviour
{
    Transform mainCamTransform; // Stores the FPS camera transform
    private bool visible = true;
    public float distanceToAppear;
    Renderer[] objRenderer;
    MeshCollider[] obCollider;
    private void Start()
    {
        mainCamTransform = Camera.main.transform;//Get camera transform reference
        objRenderer = gameObject.GetComponentsInChildren<Renderer>(); //Get render reference
        obCollider = gameObject.GetComponentsInChildren<MeshCollider>();
    }
    private void Update()
    {
        disappearChecker();
    }
    private void disappearChecker()
    {
        float distance = Vector3.Distance(mainCamTransform.position, transform.position);

        // We have reached the distance to Enable Object
        if (distance < distanceToAppear)
        {
            if (!visible)
            {
                 // Show Object
                foreach (MeshCollider turnOff in obCollider)
                {
                    turnOff.enabled = true;
                }
/*
                foreach (Renderer turnOffRend in objRenderer)
                {
                    turnOffRend.enabled = true;
                }*/
                visible = true;
                //Debug.Log("Visible");
            }
        }
        else if (visible)
        {
            //objRenderer.enabled = false; // Hide Object
            visible = false;
            foreach(MeshCollider turnOff in obCollider)
            {
                turnOff.enabled = false;
            }

          /*  foreach (Renderer turnOffRend in objRenderer)
            {
                turnOffRend.enabled = false;
            }*/

            //Debug.Log("InVisible");
        }
    }
}


