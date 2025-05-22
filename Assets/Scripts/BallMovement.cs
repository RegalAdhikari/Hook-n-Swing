using System;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private Transform spawnTransform;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float maxJumpSpeed;
    [SerializeField] private GameObject jumpDetector;
    [SerializeField] private GroundCheck groundCheck;
    
    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter = 0f;
    private Rigidbody2D rb2d;
    private float movX;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpDetector.transform.rotation = Quaternion.identity;
        jumpDetector.transform.position = new Vector3(transform.position.x, transform.position.y -0.55f, 0f);
        movX = Input.GetAxis("Horizontal");
        if(groundCheck.isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else{
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (coyoteTimeCounter>0 &&Input.GetKeyDown(KeyCode.Space))
        {
        //   Jump();
        rb2d.linearVelocityY = jumpSpeed;
        coyoteTimeCounter = 0f;
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

        // if (rb2d.linearVelocityY <Math.Abs(maxJumpSpeed))
        // {
        //     rb2d.linearVelocityY += jumpSpeed;
        // }
    }

    private void Jump()
    {
        if (groundCheck.isGrounded)
        {
            rb2d.linearVelocityY = jumpSpeed;

        }
    }
}
