using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rbPlayer;

    [SerializeField]
    private float normalSpeed = 5f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private bool isGrounded = false;
    private bool isJumping = false;
    [SerializeField]
    private float fallForce = 5f;
    // Start is called before the first frame update

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        animator = GetComponent<Animator>();   
        spriteRenderer = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(rbPlayer.velocity.x));
        animator.SetFloat("Velocity", Mathf.Abs(rbPlayer.velocity.magnitude));

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rbPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }

        if(rbPlayer.velocity.y < 2f && isJumping == true)
        {
            Debug.Log("Woow");
            rbPlayer.AddForce(Vector2.down * fallForce * Time.deltaTime, ForceMode2D.Force);
        }



    }

    private void FixedUpdate()
    {
        rbPlayer.velocity = new Vector2(Input.GetAxis("Horizontal") * normalSpeed, rbPlayer.velocity.y);
        if (rbPlayer.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rbPlayer.velocity.x > 0) 
        {
            spriteRenderer.flipX = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);

            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);

        }
    }
}
