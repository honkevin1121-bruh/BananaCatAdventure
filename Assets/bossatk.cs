using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossatk : StateMachineBehaviour
{

    Transform player;
    public float BiteAtkRange;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            if(Vector2.Distance(player.position, animator.GetComponent<Transform>().position) <= BiteAtkRange)
            {
                if(animator.GetComponent<bossDash>().collidingObjects.Count >= 2)
                {
                    animator.SetFloat("Bite", 1);
                }
                // else
                // {
                //     animator.SetFloat("Bite", 0.1f);
                // }
            }
            else
            {
                animator.SetFloat("Bite", 0);
            }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetComponent<bossDash>().collidingObjects.Count < 2)
        {  Debug.Log("bruh");
            animator.SetFloat("Bite", 0);
        }
        
    }
}
