using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Runnig : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed; 

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();   
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 newpos = Vector2.MoveTowards(rb.position, player.position, speed*Time.fixedDeltaTime);
        rb.MovePosition(newpos);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //   animator.SetFloat("RBite0", 0);
    //   animator.SetFloat("DashAtk", 0);
      
    // }

}
