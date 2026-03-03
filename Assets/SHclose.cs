using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHclose : StateMachineBehaviour
{

    Transform player;
    public float AtkRange;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector2.Distance(player.position, animator.GetComponent<Transform>().position) <= AtkRange)
        {
            animator.SetFloat("closeAtk", 1);
        }
        else
        {
            animator.SetFloat("closeAtk", 0);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("closeAtk", 1);
        
    }
}
