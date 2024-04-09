using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : StateMachineBehaviour
{
    Transform player;
    public int speed;
    public float m_coolDown;
    private float coolDown;
    Vector2 dir;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        coolDown = m_coolDown;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dir = new Vector2(player.position.x-animator.transform.position.x, player.position.y-animator.transform.position.y  );
        animator.transform.up = dir;
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, player.position, speed*Time.deltaTime);
        coolDown -= Time.deltaTime;
        if (coolDown <= 0)
        {
            coolDown = m_coolDown;
            animator.GetComponent<Boss>().Attack();

        }
        if (Vector2.Distance(animator.transform.position, player.position) <= 10f)
        {
            int randomAttack = Random.Range(1, 3);
            if(randomAttack == 1)
            {
                animator.SetTrigger("attack");
            }
            else if(randomAttack == 2)
            {
                animator.SetTrigger("attack1");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
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
