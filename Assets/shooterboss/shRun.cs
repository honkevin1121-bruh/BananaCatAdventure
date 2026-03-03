using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shRun : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed; 
    public float atkRange;
    public float LongFlyRange;
    public float ShootRange;
    public Vector3 Stillpos;
    

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
        if(Vector2.Distance(player.position, rb.position) >= LongFlyRange)
        {
            animator.SetFloat("Fly", 1);
        }
        else if(Vector2.Distance(player.position, rb.position) <= atkRange)
        {
            animator.SetFloat("closeAtk", 1);
        }
        else if(animator.GetComponent<shooterAttack>().canshooot)
        {
            if(animator.GetComponent<shootThing>().CanFS)
            {
                animator.SetFloat("shooting",1);
                animator.GetComponent<shootThing>().CanFS=false;
                animator.GetComponent<shootThing>().DuringFsh = true;
                animator.GetComponent<shootThing>().FShTimer=0;
            }
            else if(animator.GetFloat("shooting")==0)
            {
                animator.SetFloat("shoot", 1);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("Fly", 0);
        animator.SetFloat("closeAtk", 0);
        animator.SetFloat("shoot", 0);
        // if(animator.GetFloat("phase2")>1)
        // {
        //     animator.SetFloat("phase2",0.1f);
        // }
    }
}
