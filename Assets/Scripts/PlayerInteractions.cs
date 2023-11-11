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
    // Start is called before the first frame update
    void Start()
    {

        rbPlayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudioManager = GetComponent<PlayerAudio>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        light.intensity = 3f;

        if (Physics2D.Raycast(transform.position, Vector2.down, dashAttackRange, enemyMask))
        {
            light.intensity = 15f;

            if (Input.GetKeyDown(KeyCode.F))
                {
                rbPlayer.velocity = Vector2.zero;
                    rbPlayer.AddForce(Vector2.down * stompForce, ForceMode2D.Impulse);

                animator.SetTrigger("Dive");
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
            animator.SetTrigger("EnemyBounce");
        } 

    }




}
