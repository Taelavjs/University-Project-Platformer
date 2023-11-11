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

    [SerializeField]
    private Vector3 lastFellPosition;
    private Vector3 deathPosition;

    public Transform groundCheckPosition;
    [SerializeField]
    private LayerMask whatIsGround;

    private float airControl;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public GameManager gameManager;

    bool fallForceApplied;
    void Start()
    {
        animator = GetComponent<Animator>();   
        spriteRenderer = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGroundCheck();
        animator.SetFloat("Speed", Mathf.Abs(rbPlayer.velocity.x));
        animator.SetFloat("Velocity", Mathf.Abs(rbPlayer.velocity.magnitude));

        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);
            isJumping = false;
            fallForceApplied = false;
        }

        if (!isGrounded)
        {
            animator.SetBool("isGrounded", false);
        }

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rbPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }


        if (fallForceApplied == false && rbPlayer.velocity.y < -1f && isJumping == true)
        {
            //rbPlayer.AddForce(Vector2.down * fallForce * Time.deltaTime, ForceMode2D.Impulse);
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, -fallForce);
            fallForceApplied = true;

        }



    }

    private void LateUpdate()
    {
        rbPlayer.velocity = new Vector2(Mathf.Clamp(rbPlayer.velocity.x, -10f, 10f), rbPlayer.velocity.y);
    }

    private void FixedUpdate()
    {

        if(!gameManager.hitStun)
        {
            rbPlayer.velocity = new Vector2(Input.GetAxis("Horizontal") * normalSpeed, rbPlayer.velocity.y);
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rbPlayer.velocity.x > 0) 
        {
            spriteRenderer.flipX = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            Debug.Log("running");
            gameObject.transform.position = lastFellPosition;
            rbPlayer.velocity = new Vector2(0, 0);
            rbPlayer.AddForce(Vector2.up * 40f, ForceMode2D.Impulse);
        }
    }

    private bool wasGroundedPrev;
    private void isGroundCheck()
    {
        wasGroundedPrev = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, 0.1f, whatIsGround);
        if(wasGroundedPrev == true && isGrounded == false)
        {
            lastFellPosition = gameObject.transform.position;
        }
    }

}
