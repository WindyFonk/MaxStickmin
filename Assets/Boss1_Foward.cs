using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_Foward : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed = 5f;
    public float range = 9f;
    [SerializeField] private float _duration = 0.5f;
    Vector2 target;
    Vector2 newPos;
    private float distance;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = new Vector2(player.position.x, player.position.y);
        newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        distance = Vector2.Distance(newPos, target);
        FollowPlayer();

        if (distance <= range)
        {
            animator.SetTrigger("Melee");
        }

        if (distance > 70)
        {
            animator.SetBool("Rush",true);
        }


        animator.SetBool("shieldOn", animator.GetComponent<Boss1>().shieldOn);



    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Melee");
    }

    public void FollowPlayer()
    {
        rb.MovePosition(newPos);
    }

}
