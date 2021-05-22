using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mantling : MonoBehaviour
{
    [SerializeField] MantlingCollisionDetection obstruction;
    [SerializeField] MantlingCollisionDetection wall;
    [SerializeField] Transform wallClimbPosition;
    [SerializeField] float climbTime;

    private WallRunning wr;
    private Rigidbody rb;

    private Vector3 startPos;
    private Vector3 endPos;
    private bool mantleNow = false;
    private float localClimbTime;
    private float t_mantle;

    bool canMantle = false;
    public bool isWallRunning;

    private void Start()
    {
        wr = GetComponent<WallRunning>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        getValues();
        mantleCheck();
        mantleController();
    }

    void mantleCheck()
    {
        if (wall.isColliding && !obstruction.isColliding && !isWallRunning && !canMantle && !mantleNow)
        {
            canMantle = true;
        }
    }

    void getValues()
    {
        if(canMantle && Input.GetKey(KeyCode.W))
        {
            canMantle = false;
            rb.isKinematic = true;
            startPos = transform.position;
            endPos = wallClimbPosition.position;
            mantleNow = true;
            localClimbTime = climbTime;
        }
    }

    void mantleController()
    {
        if(mantleNow && t_mantle < 1f)
        {
            t_mantle += Time.deltaTime / localClimbTime;
            wr.isMantling = true;
            transform.position = Vector3.Lerp(startPos, endPos, t_mantle);

            if(t_mantle >= 1f)
            {
                mantleNow = false;
                t_mantle = 0f;
                wr.isMantling = false;
                rb.isKinematic = false;
            }
        }
    }

}
