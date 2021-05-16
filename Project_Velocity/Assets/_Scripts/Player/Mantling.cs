using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mantling : MonoBehaviour
{
    [SerializeField] MantlingCollisionDetection obstruction;
    [SerializeField] MantlingCollisionDetection wall;
    [SerializeField] Transform wallClimbPosition;
    [SerializeField] float climbSpeed;

    bool canMantle = false;
    public bool isWallRunning;

    void Update()
    {
        mantleCheck();
        mantleController();
    }

    void mantleCheck()
    {
        if (wall.isColliding && !obstruction.isColliding && !isWallRunning)
        {
            canMantle = true;
        }
        else
        {
            canMantle = false;
        }
    }

    void mantleController()
    {
        if(canMantle && Input.GetKey(KeyCode.W))
        {
            transform.position = Vector3.Lerp(transform.position, wallClimbPosition.position, climbSpeed * Time.deltaTime);
        }
        if(!canMantle)
        {
        }
    }

}
