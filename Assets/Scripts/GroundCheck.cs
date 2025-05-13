using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded;
    void Start()
    {
        isGrounded = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            isGrounded = false;
        }
    }
}
