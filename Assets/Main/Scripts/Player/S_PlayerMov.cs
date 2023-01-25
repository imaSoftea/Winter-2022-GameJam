using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerMov : MonoBehaviour
{
    [Header("Movement")]
    public float movSpeed;
    public float groundDrag;
    public float jumpForce;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    float groundLeftTime;
    float justJumped;
    bool grounded;

    [Header("Wall Running")]
    public float playerWidth;
    public LayerMask whatIsWall;
    bool walled;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    private float bumpAvoidTimer;

    public float slideYScale;
    private float startYScale;
    private KeyCode slideKey = KeyCode.LeftControl;

    public float maxSlopeAngle;
    private RaycastHit slopeHit;

    private bool isSliding;

    [Header("Other")]
    public float FreezeMaxTime;
    public float MinActivate;

    [Header("Other")]
    public Transform player;

    KeyCode jumpKey = KeyCode.Space;
    public Transform orient;
    Vector3 moveDir;

    float horzIn;
    float vertIn;

    bool doubleJumpReady;
    Rigidbody rb;

    //Start
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        startYScale = player.localScale.y;
    }

    //Updates
    void Update()
    {
        GetInput();
        CheckGround();
        CheckWall();

        //Jump
        if (Input.GetKeyDown(jumpKey) && (walled || grounded || doubleJumpReady || groundLeftTime > 0f))
        {
            AudioSource jump = GetComponent<AudioSource>();
            jump.Play();

            if ((!grounded && !walled && !(groundLeftTime > 0f)) || justJumped > 0f)
            {
                DoubleJump();
                doubleJumpReady = false;
            }
            else
            {
                Jump();
                justJumped = 0.2f;
            }
        }

        justJumped -= Time.deltaTime;

        //Sliding
        if (Input.GetKeyDown(slideKey) && (vertIn != 0 || vertIn != 0))
        {
            StartSlide();
        }

        if (Input.GetKeyUp(slideKey) && isSliding)
        {
            EndSlide();
        }

        //Wall
        if (walled)
        {
            WallClamp();
        }
    }


    void FixedUpdate()
    {
        MovePlayer();
        SpeedClamp();
        if (isSliding) Sliding();
    }

    //Input
    private void GetInput()
    {
        horzIn = Input.GetAxisRaw("Horizontal");
        vertIn = Input.GetAxisRaw("Vertical");
    }

    //Basic Movment
    private void MovePlayer()
    {
        moveDir = orient.forward * vertIn + orient.right * horzIn;

        //Add Force
        if (CheckSlope() && rb.velocity.y < 0)
        {
            rb.AddForce(GetSlopeDir() * movSpeed * 10f, ForceMode.Force);
            if (isSliding)
            {
                rb.AddForce(Vector3.ProjectOnPlane(Physics.gravity, slopeHit.normal), ForceMode.Force);
            }
            rb.useGravity = false;

            if (rb.velocity.y > 0)
                rb.AddForce(Physics.gravity, ForceMode.Force);
        }
        else if (isSliding && rb.velocity.y > 0)
        {
            rb.AddForce(moveDir.normalized * movSpeed * (5f * slideTimer * slideTimer / (maxSlideTime * maxSlideTime)), ForceMode.Force);
            rb.useGravity = true;
        }
        else if (isSliding)
        {
            rb.AddForce(moveDir.normalized * movSpeed * (18f * slideTimer * slideTimer / (maxSlideTime * maxSlideTime)), ForceMode.Force);
            rb.useGravity = true;
        }
        else if (grounded)
        {
            rb.AddForce(moveDir.normalized * movSpeed * (6f + groundDrag), ForceMode.Force);
            rb.useGravity = true;
        }
        else
        {
            rb.AddForce(moveDir.normalized * movSpeed * (5f), ForceMode.Force);
            rb.useGravity = true;
        }
    }


    //Checking
    private void CheckGround()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (grounded)
        {
            rb.drag = groundDrag;
            doubleJumpReady = true;
            groundLeftTime = 0.2f;
        }
        else
        {
            rb.drag = 0;
            groundLeftTime -= Time.deltaTime;
        }

        if (CheckSlope() && isSliding)
        {
            rb.drag = 0;
            if (rb.velocity.y > 0)
                rb.drag = groundDrag;
        }
        else if (isSliding)
        {
            if (rb.velocity.y > 0)
                rb.drag = groundDrag;
        }
    }

    private bool CheckSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.2f))
        {
            float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return slopeAngle < maxSlopeAngle && slopeAngle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeDir()
    {
        return Vector3.ProjectOnPlane(moveDir, slopeHit.normal).normalized;
    }

    private void CheckWall()
    {
        walled = Physics.Raycast(transform.position, Vector3.right, playerWidth * 0.5f + 0.2f, whatIsWall);
        if (!walled) walled = Physics.Raycast(transform.position, Vector3.left, playerWidth * 0.5f + 0.2f, whatIsWall);
        if (!walled) walled = Physics.Raycast(transform.position, Vector3.forward, playerWidth * 0.5f + 0.2f, whatIsWall);
        if (!walled) walled = Physics.Raycast(transform.position, Vector3.back, playerWidth * 0.5f + 0.2f, whatIsWall);

        if (walled) 
        {
            groundLeftTime = 0.2f;
            doubleJumpReady = true;
        }
    }


    //Clamping
    private void SpeedClamp()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > movSpeed)
        {
            rb.AddForce(-flatVel.normalized * (rb.velocity.magnitude - movSpeed) * (rb.velocity.magnitude - movSpeed) * 1f, ForceMode.Force);
        }
    }

    private void WallClamp()
    {
        float momentum = rb.velocity.y;
        if (momentum < -0.3f) momentum = -0.3f;

        rb.velocity = new Vector3(rb.velocity.x, momentum, rb.velocity.z);
    }


    //Jumping
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void DoubleJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce * 1f, ForceMode.Impulse);
    }


    //Sliding
    private void StartSlide()
    {
        isSliding = true;

        player.localScale = new Vector3(player.localScale.x, slideYScale, player.localScale.z);
        rb.AddForce(Vector3.down * 4f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void EndSlide()
    {
        isSliding = false;
        player.localScale = new Vector3(player.localScale.x, startYScale, player.localScale.z);
    }

    private void Sliding()
    {
        Vector3 inputDirection = orient.forward * vertIn + orient.right * horzIn;
        slideTimer -= Time.deltaTime;

        if (CheckSlope())
        {
            slideTimer = maxSlideTime;
        }

        if (slideTimer < 0) EndSlide();
    }
}
