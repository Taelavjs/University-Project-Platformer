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

    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            

            if (Physics2D.Raycast(transform.position, Vector2.down, dashAttackRange, enemyMask))
                {
                    rbPlayer.AddForce(Vector2.down * stompForce, ForceMode2D.Impulse);
                
                StartCoroutine(DashAttackAnimation());
            }


        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 0);
            rbPlayer.AddForce(Vector2.up * killJumpForce, ForceMode2D.Impulse);
            StartCoroutine(HitEnemy());
        }
    }

    IEnumerator DashAttackAnimation()
    {
        animator.SetBool("isStomp", true);
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("isStomp", false);

    }

    IEnumerator HitEnemy()
    {
        animator.SetBool("enemyHit", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("enemyHit", false);
    }
}
