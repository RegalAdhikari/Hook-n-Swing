using System;
using UnityEngine;
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
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        dragDistance = Screen.height * 20 / 100; //dragDistance is 15% height of the screen

    }


    void Update()
    {
        TouchUpdate();
        jumpDetector.transform.rotation = Quaternion.identity;
        jumpDetector.transform.position = new Vector3(transform.position.x, transform.position.y - 0.55f, 0f);
        // movX = Input.GetAxis("Horizontal"); // Comment out for release
        movX = moveValue;
        if (groundCheck.isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            //   Jump();
            rb2d.linearVelocityY = jumpSpeed;
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
        }
    }

    private void FixedUpdate()
    {
        Move();
        // If the ball goes too down
        if (transform.position.y <= -10f)
        {
            transform.position = spawnTransform.position;

            rb2d.linearVelocity = Vector3.zero;
            rb2d.angularVelocity = 0f;
        }
    }

    private void Move()
    {
        if (rb2d.linearVelocityX < Math.Abs(maxMoveSpeed))
        {
            rb2d.linearVelocityX += moveSpeed * movX * Time.fixedDeltaTime;
        }
        else
        {
            rb2d.linearVelocityX = maxMoveSpeed;
        }
    }

    public void Jump()
    {
        // if (groundCheck.isGrounded)
        // {
        //     rb2d.linearVelocityY = jumpSpeed;
        // }
        jumpBufferCounter = jumpBufferTime;

    }
    public void movePress(float value)
    {
        moveValue = value;
    }
    public void moveRelease()
    {
        moveValue = 0;
    }
    private void TouchUpdate()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase != TouchPhase.Stationary)
            {
                moveValue = 0;
            }
            if (touch.phase == TouchPhase.Began) //check for the first touch
                {
                    fp = touch.position;
                    lp = touch.position;
                    if (fp.x < Screen.width / 2)
                    {
                        // Debug.Log("Move left");
                        moveValue = -1;
                    }
                    else
                    {
                        // Debug.Log("Move right");
                        moveValue = 1;
                    }
                }

                else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
                {
                    lp = touch.position;
                    moveValue = 0;

                }
                else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
                {
                    moveValue = 0;

                    lp = touch.position;  //last touch position. Ommitted if you use list

                    //Check if drag distance is greater than 20% of the screen height
                    if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                    {//It's a drag
                     //check if the drag is vertical or horizontal
                        if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                        {   //If the horizontal movement is greater than the vertical movement...
                            // if (lp.x > fp.x)  //If the movement was to the right)
                            // {   //Right swipe
                            //     Debug.Log("Right Swipe");
                            // }
                            // else
                            // {   //Left swipe
                            //     Debug.Log("Left Swipe");
                            // }
                        }
                        else
                        {   //the vertical movement is greater than the horizontal movement
                            if (lp.y > fp.y)  //If the movement was up
                            {   //Up swipe
                                // Debug.Log("Up Swipe");
                                jumpBufferCounter = jumpBufferTime;

                            }
                        }
                    }
                    else
                    {   //It's a tap as the drag distance is less than 20% of the screen height
                        moveValue = 0;
                        // Debug.Log("Tap");
                    }

                }
        }
    }
}