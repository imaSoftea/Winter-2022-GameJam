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

    [Header("Other")]
    //Orientation
    KeyCode jumpKey = KeyCode.Space;
    public Transform orient;
    Vector3 moveDir;

    //Input
    float horzIn;
    float vertIn;

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
    }
    
    void FixedUpdate()
    {
        MovePlayer();
        if(Input.GetKey(jumpKey) && grounded)
        {
            Jump();
        }

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

        if (grounded) rb.drag = groundDrag;
        else rb.drag = 0;
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

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
