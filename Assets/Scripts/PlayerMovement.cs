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
    [SerializeField]
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


    public bool fallForceApplied;

    float originalBuffer;
    float bufferInputTime = 0;

    void Start()
    {
        originalBuffer = gameManager.bufferInput;

        animator = GetComponent<Animator>();   
        spriteRenderer = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            bufferInputTime = originalBuffer;
        }
        else
        {
            bufferInputTime -= Time.deltaTime;
        }

        isGroundCheck();
        animator.SetFloat("Speed", Mathf.Abs(rbPlayer.velocity.x));
        animator.SetFloat("Velocity", Mathf.Abs(rbPlayer.velocity.magnitude));

        if (isGrounded)
        {
            gameManager.resetKillCombo();
            animator.SetBool("isGrounded", true);
            isJumping = false;
            fallForceApplied = false;
        }

        if (!isGrounded)
        {
            animator.SetBool("isGrounded", false);
        }

        if (bufferInputTime > 0 && isGrounded)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 0f);
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
    public float deacceleration;
    public float maxSpeed;
    private void LateUpdate()
    {
        rbPlayer.velocity = new Vector2(Mathf.Clamp(rbPlayer.velocity.x, -maxSpeed, maxSpeed), rbPlayer.velocity.y);

        if(Input.GetAxis("Horizontal") == 0)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x * deacceleration, rbPlayer.velocity.y);
        }
    }

    private void FixedUpdate()
    {

        if(!gameManager.hitStun)
        {
            //rbPlayer.velocity = new Vector2(Input.GetAxis("Horizontal") * normalSpeed, rbPlayer.velocity.y);
            rbPlayer.AddForce(Vector2.right * Input.GetAxis("Horizontal") * normalSpeed);

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
            gameManager.GetComponent<GameManager>().SetStateRespawn();
            animator.SetTrigger("Death");
        }

        if (collision.gameObject.CompareTag("Snow"))
        {
            lastFellPosition = collision.transform.position;
        }
    }

    private bool wasGroundedPrev;
    private void isGroundCheck()
    {
        wasGroundedPrev = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, 0.1f, whatIsGround);

    }

    public void RespawnPlayer()
    {
        Debug.Log("running");
        gameObject.transform.position = lastFellPosition;
        rbPlayer.velocity = new Vector2(0, 0);
        rbPlayer.AddForce(Vector2.up * 40f, ForceMode2D.Impulse);
    }



}
