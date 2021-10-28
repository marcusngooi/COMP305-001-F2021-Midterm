using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // "Public" variables
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 500.0f;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask whatIsGround;

    // Private variables
    private Rigidbody2D rBody;
    private Animator anim;
    private bool isGrounded = false;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Physics
    void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        isGrounded = GroundCheck();

        // Jump code
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            rBody.AddForce(new Vector2(0.0f, jumpForce));
            isGrounded = false;
        }

        // Duck code
        if (isGrounded && Input.GetAxis("Vertical") < 0)
        {
            anim.SetBool("isDucking", true);
        }
        else
        {
            anim.SetBool("isDucking", false);
        }

        // Run code
        rBody.velocity = new Vector2(horiz * speed, rBody.velocity.y);

        // Check if the sprite needs to be flipped
        if ((isFacingRight && rBody.velocity.x < 0) || (!isFacingRight && rBody.velocity.x > 0))
        {
            Flip();
        }

        // Communicate with the animator
        anim.SetFloat("xSpeed", Mathf.Abs(rBody.velocity.x));
        anim.SetFloat("ySpeed", rBody.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
    }

    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;

        isFacingRight = !isFacingRight;
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    // "Kill" player if they touch a spike
    //    if (other.gameObject.CompareTag("Spike"))
    //    {
    //        this.transform.position = new Vector3(-5.0f, -0.5700001f, 0.0f);
    //    }

    //    // Prevent player from sliding off platform
    //    if (other.gameObject.CompareTag("Platform"))
    //    {
    //        this.transform.parent = other.transform;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Platform"))
    //    {
    //        this.transform.parent = null;
    //    }
    //}
}
