using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Shooting shooting;
    [SerializeField] private List<Camera> camList = new List<Camera>();
    [SerializeField] Transform orientation;

    //Current Speed
    public float currentSpeed;

    //Wall Running Drag
    public bool isWallRunning;

    //Gravity
    [SerializeField] float gravityScale = 1f;
    public bool useGravity;

    [Header("World Boundaries")]
    [SerializeField] float upperBound;
    [SerializeField] float lowerBound;

    [Header("Camera Effects")]
    [SerializeField] float fov;
    [SerializeField] float slideFov;
    [SerializeField] float slideFovTime;

    [Header("Movement")]
    [SerializeField] float universalMaxVelocity;
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
    [SerializeField] float startSlideSpeed;
    [SerializeField] float slideDecay;
    [SerializeField] float slideCD;
    [SerializeField] float crouchSpeed;
    float slideCDTimer;
    float slideSpeed;

    Vector3 slideDirection;
    Vector3 slopeSlideDirection;

    [SerializeField] float standingHeight = 1f;
    [SerializeField] float crouchingHeight = 0.5f;
    float playerHeight = 2f;


    bool isCrouching = false;
    bool isSliding = false;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.4f;
    float groundCheckDelay = 0.1f;
    float groundCheckTimer;

    //turn back to private after debug
    public bool onGround = true;
    public bool isGrounded;

    RaycastHit slopeHit;
    public bool onSlope;

    [HideInInspector]
    public int pressedTimes;
    private bool OnSlope()
    {
        if(!shooting.reverseGravity)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 1))
            {
                if (slopeHit.normal != Vector3.up)
                {
                    onSlope = true;
                    return true;
                }
                else
                {
                    onSlope = false;
                    return false;
                }
            }
        }
        else if(shooting.reverseGravity)
        {
            if (Physics.Raycast(transform.position, Vector3.up, out slopeHit, playerHeight / 2 + 1))
            {
                if (slopeHit.normal != Vector3.down)
                {
                    onSlope = true;
                    return true;
                }
                else
                {
                    onSlope = false;
                    return false;
                }
            }
        }
        onSlope = false;
        return false;
    }

    private void Start()
    {
        useGravity = true;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        shooting = GetComponent<Shooting>();

        coyoteTimer = coyoteTime;
        slideSpeed = startSlideSpeed;
    }

    private void Update()
    {
        currentSpeed = rb.velocity.magnitude;
        WorldBoundaries();
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
        if(shooting.reverseGravity)
        {
            if (useGravity) rb.AddForce(Physics.gravity * (rb.mass * rb.mass) * -gravityScale);
        }
        else if(!shooting.reverseGravity)
        {
            if (useGravity) rb.AddForce(Physics.gravity * (rb.mass * rb.mass) * gravityScale);
        }
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
        //if below max speed
        if(currentSpeed < universalMaxVelocity)
        {
            //On ground or wall running and not on slope
            if ((isGrounded && !OnSlope()) || (isWallRunning && !OnSlope()))
            {
                if (isCrouching)
                {
                    rb.AddForce(moveDirection.normalized * crouchSpeed * movementMultiplier, ForceMode.Acceleration);
                }
                else if (isSliding)
                {
                    rb.AddForce(slideDirection.normalized * slideSpeed * movementMultiplier, ForceMode.VelocityChange);
                }
                else if (!isCrouching && !isSliding)
                {
                    rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
                }
            }
            //On ground and on slope and not wall running
            else if (isGrounded && OnSlope())
            {
                if (isCrouching)
                {
                    rb.AddForce(slopeMoveDirection.normalized * crouchSpeed * movementMultiplier, ForceMode.Acceleration);
                }
                else if (isSliding)
                {
                    rb.AddForce(slopeSlideDirection.normalized * slideSpeed * movementMultiplier, ForceMode.VelocityChange);
                }
                else if (!isCrouching && !isSliding)
                {
                    rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
                }
            }
            //not on ground
            else if (!isGrounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
            }
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
        if(isGrounded && !isWallRunning && currentSpeed < slideThreshold && Input.GetKeyDown(KeyCode.LeftShift) && !isSliding)
        {
            if (slideCDTimer <= 0)
            {
                isCrouching = true;
                slideCDTimer = slideCD;
            }
        }
        //Start Sliding
        else if(isGrounded && !isWallRunning && currentSpeed >= slideThreshold && Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
        {
            if(slideCDTimer <= 0)
            {
                isSliding = true;
                slideDirection = moveDirection;
                slopeSlideDirection = slopeMoveDirection;
                slideCDTimer = slideCD;
            }
        }
        //Stand up
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
        }
    }

    void Sliding()
    {
        if(isSliding)
        {
            foreach(Camera cam in camList)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, slideFov, slideFovTime * Time.deltaTime);
            }
            transform.localScale = new Vector3(transform.localScale.x, crouchingHeight, transform.localScale.z);
            if(!shooting.reverseGravity)
            {
                //Normal grav
                if (!OnSlope() || (rb.velocity.y > 0))
                {
                    slideSpeed = Mathf.Lerp(slideSpeed, 0, slideDecay * Time.deltaTime);
                }
            }
            else if(shooting.reverseGravity)
            {
                //rev grav
                if (!OnSlope() || (rb.velocity.y < 0))
                {
                    slideSpeed = Mathf.Lerp(slideSpeed, 0, slideDecay * Time.deltaTime);
                }
            }
        }
    }

    void StandUp()
    {
        if(!isCrouching && !isSliding)
        {
            foreach(Camera cam in camList)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, slideFovTime * Time.deltaTime);
            }
            transform.localScale = new Vector3(transform.localScale.x, standingHeight, transform.localScale.z);
            slideSpeed = startSlideSpeed;
            if(slideCDTimer > -1)
            {
                slideCDTimer -= Time.deltaTime;
            }
        }
    }

    void WorldBoundaries()
    {
        if(transform.position.y > upperBound || transform.position.y < lowerBound)
        {
            SceneManager.LoadScene("Movement_Playtest_Map");
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
