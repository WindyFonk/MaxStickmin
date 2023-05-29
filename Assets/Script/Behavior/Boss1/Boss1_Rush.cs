using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_Rush : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed = 35f;
    public float range = 5f;
    Vector2 target;
    Vector2 newPos;
    private float distance;
    private AudioSource rushSound;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rushSound = animator.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        rushSound.enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = new Vector2(player.position.x, player.position.y);
        newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        distance = Mathf.Abs( newPos.x - target.x);

        

        rb.MovePosition(newPos);
        if (distance <= range)
        {
            animator.SetTrigger("Melee");
            animator.SetBool("Rush", false);
        }

        Debug.Log(distance);

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Rush",false);
        rushSound.enabled = false;
    }



    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
