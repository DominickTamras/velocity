using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform pt;
    Rigidbody rb;
    [SerializeField] Transform orientation;

    //Current Speed
    public float currentSpeed;

    //Wall Running Drag
    public bool isWallRunning;

    //Gravity
    [SerializeField] float gravityScale = 1f;
    public bool useGravity;

    [Header("Movement")]
    public float minSpeed = 6f;
    public float maxSpeed = 10f;
    public float acceleration = 1f;
    public float movementMultiplier = 10f;
    public float airMultiplier = 0.4f;
    public float groundDrag = 6f;
    public float slideDrag = 0.5f;
    public float airDrag = 1f;
    float moveSpeed;
    bool isMoving;

    float horizontalMovement;
    float verticalMovement;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    [Header("Jumping")]
    public float jumpForce = 15f;
    [SerializeField] float coyoteTime;
    float coyoteTimer;

    [Header("Crouching and Sliding")]
    [SerializeField] float slideThreshold;
    [SerializeField] float slideSpeed;
    [SerializeField] float crouchSpeed;

    [SerializeField] float standingHeight = 1f;
    [SerializeField] float crouchingHeight = 0.5f;
    float playerHeight = 2f;


    public bool isCrouching = false;
    public bool isSliding = false;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    float groundCheckRadius = 0.4f;
    float groundCheckDelay = 0.1f;
    float groundCheckTimer;

    //turn back to private after debug
    public bool onGround = true;
    public bool isGrounded;

    RaycastHit slopeHit;
    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        useGravity = true;

        pt = GetComponent<Transform>();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        coyoteTimer = coyoteTime;
    }

    private void Update()
    {
        currentSpeed = rb.velocity.magnitude;
        CrouchAndSlideManager();
        Crouching();
        Sliding();
        StandUp();

        GroundCheck();

        PlayerInput();
        ControlDrag();

        Accelerate();

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Jump();
        }

        //Populates slopeMoveDirection with ProjectOnPlane which projects moveDirection onto a surface (in this case: slopeHit).
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    private void FixedUpdate()
    {
        MovePlayer();

        //gravity
        rb.useGravity = false;
        if (useGravity) rb.AddForce(Physics.gravity * (rb.mass * rb.mass) * gravityScale);
    }

    //Gets the inputs from the player and populates moveDirection.
    void PlayerInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    //Handles moving the player.
    void MovePlayer()
    {
        if((isGrounded && !OnSlope()) || (isWallRunning && !OnSlope()))
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if(isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if(!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }

    }

    //Handles the drag of the player.
    void ControlDrag()
    {
        if((isGrounded && !isSliding) || (isWallRunning && !isSliding))
        {
            rb.drag = groundDrag;
        }
        else if(isSliding)
        {
            rb.drag = slideDrag;
        }
        else if(!isGrounded || !isWallRunning)
        {
            rb.drag = airDrag;
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        onGround = false;
        isGrounded = false;
        groundCheckTimer = groundCheckDelay;
    }

    void GroundCheck()
    {
        if(groundCheckTimer <= 0)
        {
            onGround = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        }
        else if(groundCheckTimer >= 0)
        {
            groundCheckTimer -= Time.deltaTime;
        }

        if (onGround)
        {
            isGrounded = true;
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
            if(coyoteTimer <= 0)
            {
                isGrounded = false;
            }
        }
    }

    void Accelerate()
    {
        if(isMoving)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = minSpeed;
        }
    }

    void CrouchAndSlideManager()
    {
        //Start Crouching
        if(isGrounded && !isWallRunning && currentSpeed < slideThreshold && Input.GetKey(KeyCode.LeftShift))
        {
            isCrouching = true;
        }
        else if(isGrounded && !isWallRunning && currentSpeed >= slideThreshold && Input.GetKey(KeyCode.LeftShift))
        {
            isSliding = true;
        }
        else if(!isGrounded || Input.GetKeyUp(KeyCode.LeftShift))
        {
            isCrouching = false;
            isSliding = false;
        }
    }

    void Crouching()
    {
        if(isCrouching)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchingHeight, transform.localScale.z);
            //Movement change
        }
    }

    void Sliding()
    {
        if(isSliding)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchingHeight, transform.localScale.z);
            //Movement change
        }
    }

    void StandUp()
    {
        if(!isCrouching && !isSliding)
        {
            transform.localScale = new Vector3(transform.localScale.x, standingHeight, transform.localScale.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    public Vector3 CalcFuturePost(float timeinSeconds)
    {
        Vector3 pos = transform.position;
        pos += rb.velocity * timeinSeconds;

        return pos;
    }
}
