using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Header("References")]
    [SerializeField] WallRunning wr;
    Shooting s;

    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;

    float mouseX;
    float mouseY;

    //Overall sensitivity
    float multiplier = 1f;

    float xRotation;
    float yRotation;
    public float zRotation;

    private void Start()
    {
        s = GetComponent<Shooting>();
    }

    private void Update()
    {
        if(!MenuManager.GameIsPaused)
        {
            PlayerInput();

            orientation.transform.localRotation = Quaternion.Euler(0, yRotation, zRotation);
            cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation + wr.tilt);
        }
    }

    void PlayerInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        if(s.reverseGravity)
        {
            yRotation += -mouseX * sensX * multiplier * Time.fixedDeltaTime;
            xRotation -= -mouseY * sensY * multiplier * Time.fixedDeltaTime;
        }
        else if(!s.reverseGravity)
        {
            yRotation += mouseX * sensX * multiplier * Time.fixedDeltaTime;
            xRotation -= mouseY * sensY * multiplier * Time.fixedDeltaTime;
        }

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }
}
