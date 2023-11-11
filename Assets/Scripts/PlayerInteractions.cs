using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            

            if (Physics2D.Raycast(transform.position, Vector2.down, dashAttackRange, enemyMask))
                {
                    rbPlayer.AddForce(Vector2.down * stompForce, ForceMode2D.Impulse);

                animator.SetTrigger("Dive");
            }


        }

    }
    bool isEnemyBelow = false;
    public Transform groundCheckPosition;
    public float enemyHitAttack;
    public float yangleFlyBack;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isEnemyBelow = Physics2D.OverlapCircle(groundCheckPosition.position, 0.3f, enemyMask);

        if (isEnemyBelow)
        {
            animator.SetTrigger("EnemyBounce");
        } else if(collision.gameObject.CompareTag("Enemy"))
        {
            rbPlayer.AddForce((transform.position - collision.gameObject.transform.position + new Vector3(0, yangleFlyBack, 0)).normalized * enemyHitAttack, ForceMode2D.Impulse);
            StartCoroutine(gameManager.playerHitStun());
        }

    }




}
