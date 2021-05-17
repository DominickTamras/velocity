using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mantling : MonoBehaviour
{
    [SerializeField] MantlingCollisionDetection obstruction;
    [SerializeField] MantlingCollisionDetection wall;
    [SerializeField] Transform wallClimbPosition;
    [SerializeField] float climbSpeed;

    private WallRunning wr;

    bool canMantle = false;
    public bool isWallRunning;

    private void Start()
    {
        wr = GetComponent<WallRunning>();
    }

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
            wr.isMantling = true;
            transform.position = Vector3.Lerp(transform.position, wallClimbPosition.position, climbSpeed * Time.deltaTime);
        }
        if(!canMantle)
        {
            wr.isMantling = false;
        }
    }

}
