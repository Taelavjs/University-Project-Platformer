using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rbPlayer;

    [Header("Movement Values")]
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
    [SerializeField]
    float stompForce;

    [SerializeField]
    private float dashAttackRange;

    [Header("Buffer System")]
    public float originalBuffer;
    public float bufferInputTime;

    public float originalBufferF;
    public float bufferInputTimeF;

    [Header("Game Objects")]
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public GameManager gameManager;
    public Transform groundCheckPosition;

    public Light2D light;


    [Header("Camera Shake Values")]
    public Transform Camera;
    public Vector3 camShakeVector;
    public float shakeTime;

    [Header("Bools")]
    public bool fallForceApplied;
    private bool wasGroundedPrev;
    bool isEnemyBelow = false;


    [Header("Float")]
    public float deacceleration;
    public float maxSpeed;

    [Header("Ects")]
    [SerializeField]
    private Vector3 lastFellPosition;
    private Vector3 deathPosition;

    [SerializeField]
    private LayerMask whatIsGround;
    public LayerMask enemyMask;
    public PlayerAudio playerAudioManager;

    // Start is called before the first frame update


    void Start()
    {
        originalBuffer = gameManager.bufferInput;

        //Grabbing Objects
        animator = GetComponent<Animator>();   
        spriteRenderer = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
        playerAudioManager = GetComponent<PlayerAudio>();
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
        animator.SetFloat("Speed", Mathf.Abs(rbPlayer.velocity.x));
        animator.SetFloat("Velocity", Mathf.Abs(rbPlayer.velocity.magnitude));
        //IsGrounded
        isGroundCheck();
        if (isGrounded)
        {
            resetKillCombo();
            animator.SetBool("isGrounded", true);
            isJumping = false;
            fallForceApplied = false;
        }
        if (bufferInputTime > 0 && isGrounded)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 0f);
            rbPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }

        //NotGrounded
        if (!isGrounded)
        {
            animator.SetBool("isGrounded", false);
        }

        //Jumping
        if (fallForceApplied == false && rbPlayer.velocity.y < -1f && isJumping == true)
        {
            //rbPlayer.AddForce(Vector2.down * fallForce * Time.deltaTime, ForceMode2D.Impulse);
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, -fallForce);
            fallForceApplied = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            bufferInputTimeF = originalBufferF;
        }
        else
        {
            bufferInputTimeF -= Time.deltaTime;
        }

        light.intensity = 0f;

        if (Physics2D.Raycast(transform.position, Vector2.down, dashAttackRange, enemyMask))
        {
            light.intensity = 30f;

            if (bufferInputTimeF > 0)
            {
                animator.SetTrigger("Dive");
                rbPlayer.AddForce(Vector2.down * stompForce, ForceMode2D.Impulse);
                rbPlayer.velocity = new Vector2(0, stompForce);
                iTween.ShakePosition(Camera.gameObject, new Vector3(0.1f, 0.1f, 0f), 0.25f);

            }


        }

    }

    private void LateUpdate()
    {
        // Caps Max Speed
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

        bool isEnemyBelow = Physics2D.OverlapCircle(groundCheckPosition.position, 0.3f, enemyMask);

        if (isEnemyBelow || collision.gameObject.CompareTag("Enemy"))
        {

            iTween.ShakePosition(Camera.gameObject, camShakeVector, shakeTime);
            increaseKillCombo();
            animator.SetTrigger("EnemyBounce");
        }


        if (collision.gameObject.CompareTag("Goal"))
        {
            gameManager.nextLevel();
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Goal"))
        {
            gameManager.nextLevel();
        }
    }

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

    public int killCombo;

    public void increaseKillCombo()
    {
        killCombo++;
    }

    public void resetKillCombo()
    {
        killCombo = 0;
    }

    public int getKillCombo()
    {
        return killCombo;
    }
}
