using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Transform orientation;

    //Gravity
    [SerializeField] float gravityScale = 1f;
    bool useGravity = true;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float movementMultiplier = 10f;
    public float airMultiplier = 0.4f;
    public float groundDrag = 6f;
    public float airDrag = 2f;

    float horizontalMovement;
    float verticalMovement;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    [Header("Jumping")]
    public float jumpForce = 15f;
    [SerializeField] float coyoteTime;
    float coyoteTimer;
    float playerHeight = 2f;

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
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        coyoteTimer = coyoteTime;
    }

    private void Update()
    {
        GroundCheck();

        PlayerInput();
        ControlDrag();

        //Jumping
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
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

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    //Handles moving the player.
    void MovePlayer()
    {
        if(isGrounded && !OnSlope())
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
        if(isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
