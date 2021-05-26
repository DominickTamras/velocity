using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float interactRange;

    void Start()
    {

    }

    void Update()
    {
        InteractFinder();
    }

    void InteractFinder()
    {
        //Raycast
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactRange))
        {
            //If a terminal is found
            TerminalObject termObj = hit.transform.GetComponent<TerminalObject>();
            if(termObj != null)
            {
                //Show interact key popup
                //Allow interact with terminal
                //If interact, use the menumanager to open terminal menu
                if(Input.GetKeyDown(KeyCode.E))
                {
                    termObj.OpenTerminal();
                }
            }
        }
    }
}
