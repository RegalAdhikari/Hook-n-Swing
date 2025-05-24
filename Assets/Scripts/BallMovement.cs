using System;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float maxJumpSpeed;

    public float moveValue;

    [SerializeField] private GameObject jumpDetector;
    [SerializeField] private GroundCheck groundCheck;
    

    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter = 0f;
    private float jumpBufferCounter;
    private float jumpBufferTime = 0.2f; 
    private Rigidbody2D rb2d;
    private float movX;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        
        jumpDetector.transform.rotation = Quaternion.identity;
        jumpDetector.transform.position = new Vector3(transform.position.x, transform.position.y -0.55f, 0f);
        // movX = Input.GetAxis("Horizontal"); // Comment out for release
        movX = moveValue;
        if(groundCheck.isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else{
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter-=Time.deltaTime;
        }
        
    }

    private void FixedUpdate()
    {
        Move();
        // If the ball goes too down
        if (transform.position.y<=-10f)
        {
            rb2d.linearVelocity = Vector3.zero;
            transform.position = spawnTransform.position;
        }
    }

    private void Move()
    {
        if (rb2d.linearVelocityX < Math.Abs(maxMoveSpeed))
        {
            rb2d.linearVelocityX += moveSpeed * movX* Time.fixedDeltaTime;
        }
        else
        {
            rb2d.linearVelocityX = maxMoveSpeed;
        }
    }

    public void Jump()
    {
        if (coyoteTimeCounter>0f && jumpBufferCounter>0f)
        {
        rb2d.linearVelocityY = jumpSpeed;
        coyoteTimeCounter = 0f;
        jumpBufferCounter = 0f;
        }
        // if (groundCheck.isGrounded)
        // {
        //     rb2d.linearVelocityY = jumpSpeed;
        // }
    }
    public void movePress(float value){
        moveValue = value;
    }
    public void moveRelease(){
        moveValue = 0;
    }
}