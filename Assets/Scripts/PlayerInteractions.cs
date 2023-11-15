using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class PlayerInteractions : MonoBehaviour
{

    private Rigidbody2D rbPlayer;
    [SerializeField]
    private float dashAttackRange;
    [SerializeField]
    private LayerMask enemyMask;


    [SerializeField]
    float stompForce;

    [SerializeField]
    float killJumpForce;

    private Animator animator;

    private PlayerAudio playerAudioManager;

    public Light2D light;

    RaycastHit2D hit;
    public GameManager gameManager;
    public Transform Camera;

    public Vector3 camShakeVector;
    public float shakeTime;

    float originalBuffer;
    float bufferInputTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        originalBuffer = gameManager.bufferInput;
        rbPlayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudioManager = GetComponent<PlayerAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            bufferInputTime = originalBuffer;
        } else
        {
            bufferInputTime -= Time.deltaTime;
        }

        light.intensity = 0f;

        if (Physics2D.Raycast(transform.position, Vector2.down, dashAttackRange, enemyMask))
        {
            light.intensity = 30f;

            if (bufferInputTime > 0)
            {
                animator.SetTrigger("Dive");
                //rbPlayer.AddForce(Vector2.down * stompForce, ForceMode2D.Impulse);
                rbPlayer.velocity = new Vector2(0, stompForce);
                iTween.ShakePosition(Camera.gameObject, new Vector3(0.1f, 0.1f, 0f), 0.25f);
                
            }


        }

    }
    bool isEnemyBelow = false;
    public Transform groundCheckPosition;
    public float enemyHitAttack;
    public float yangleFlyBack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isEnemyBelow = Physics2D.OverlapCircle(groundCheckPosition.position, 0.3f, enemyMask);

        if (isEnemyBelow || collision.gameObject.CompareTag("Enemy"))
        {

            iTween.ShakePosition(Camera.gameObject, camShakeVector, shakeTime);
            gameManager.increaseKillCombo();
            animator.SetTrigger("EnemyBounce");
        }


        if (collision.gameObject.CompareTag("Goal"))
        {
            gameManager.nextLevel();
        }

        
    }




}
