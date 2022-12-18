using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerMov : MonoBehaviour
{
    [Header("Movement")]
    //Movement Speed
    public float movSpeed;
    public float groundDrag;
    public float jumpForce;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Wall Running")]
    public float playerWidth;
    public LayerMask whatIsWall;
    bool walled;

    [Header("Other")]
    //Orientation
    KeyCode jumpKey = KeyCode.Space;
    public Transform orient;
    Vector3 moveDir;

    //Input
    float horzIn;
    float vertIn;

    //Extras
    bool doubleJumpReady;

    //RigidBody
    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        GetInput();
        SpeedClamp();
        CheckGround();
        CheckWall();

        //Jump
        if (Input.GetKeyDown(jumpKey) && (grounded || doubleJumpReady))
        {
            if (!grounded)
            {
                DoubleJump();
                doubleJumpReady = false;
            }
            else
            {
                Jump();
            }
        }

        //Wall
        if(walled)
        {
            WallClamp();
            Debug.Log("Riding Wall");
        }
    }
    
    void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        horzIn = Input.GetAxisRaw("Horizontal");
        vertIn = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //Add Force
        moveDir = orient.forward * vertIn + orient.right * horzIn;
        rb.AddForce(moveDir.normalized * movSpeed * 10f, ForceMode.Force);
    }

    private void CheckGround()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (grounded)
        {
            rb.drag = groundDrag;
            doubleJumpReady = true;
        }
        else rb.drag = 0;
    }

    private void CheckWall()
    {
        walled = Physics.Raycast(transform.position, Vector3.right, playerWidth * 0.5f + 0.2f, whatIsWall);
        if(!walled) walled = Physics.Raycast(transform.position, Vector3.left, playerWidth * 0.5f + 0.2f, whatIsWall);
    }

    private void SpeedClamp()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > movSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void WallClamp()
    {
        float momentum = rb.velocity.y;
        if (momentum < 0f) momentum = -0.6f;

        rb.velocity = new Vector3(rb.velocity.x, momentum, rb.velocity.z);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void DoubleJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce * 0.80f, ForceMode.Impulse);
    }
}
