using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHitBounce : StateMachineBehaviour
{
    Rigidbody2D rbPlayer;
    [SerializeField]
    PlayerMovement playerMovement;
    AudioSource hitSfxBouncing;
    AudioSource[] sources;
    [SerializeField]
    float killJumpForce;

    GameManager gameManager;
    public float pitchIncrease;

    private float originalPitch;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        sources = GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>();
        hitSfxBouncing = sources[0];
        rbPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        hitSfxBouncing = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

        rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 0);
        rbPlayer.AddForce(Vector2.up * killJumpForce, ForceMode2D.Impulse);

        originalPitch = hitSfxBouncing.pitch;

        hitSfxBouncing.pitch += playerMovement.getKillCombo() * pitchIncrease;
        hitSfxBouncing.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("EnemyBounce");
        hitSfxBouncing.pitch = originalPitch;

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
}
