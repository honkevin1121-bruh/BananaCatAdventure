using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class FaceBossRun : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed; 
    public float atkRange;
    public float LongDashRange;
    public float suckRange;
    List<bool> canskill;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();   
       canskill = animator.GetComponent<bossDash>().canskill;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 newpos = Vector2.MoveTowards(rb.position, player.position, speed*Time.fixedDeltaTime);
        rb.MovePosition(newpos);
        if(Vector2.Distance(player.position, rb.position) <= atkRange)
        {
            if(animator.GetComponent<bossHealth>().isPhase2)
            {
                if(canskill[1])
                {
                    if(!animator.GetComponent<bossDash>().duringAtk)
                    {
                        canskill[1]=false;
                        animator.SetFloat("atkindex", 1);
                        animator.GetComponent<bossDash>().duringAtk=true;
                    }
                }
                else if(!animator.GetComponent<bossDash>().duringAtk)
                {
                    animator.SetFloat("DashAtk", 1f);
                }
            }
            else if(!animator.GetComponent<bossDash>().duringAtk)
            {
                animator.SetFloat("DashAtk", 1f);
            }
            
        }
        else if(Vector2.Distance(player.position, rb.position) >= LongDashRange&&!animator.GetComponent<bossDash>().duringAtk)
        {
            // animator.SetFloat("DashAtk", -1);
        }
        else
        {
            for(int i=0;i<4&&!animator.GetComponent<bossDash>().duringAtk&&animator.GetComponent<bossHealth>().isPhase2;i++)
            {
                if(canskill[i])
                {
                    if(i==0&&Vector2.Distance(player.position, rb.position) <=suckRange)
                    {
                        Debug.Log("000000000000000000000000000000000000==========");
                        canskill[i]=false;
                        animator.SetFloat("atkindex", i);
                        animator.GetComponent<bossDash>().duringAtk=true;
                    }
                    else if(i==3&&Vector2.Distance(player.position, rb.position) >suckRange)
                    {
                        Debug.Log("3333333333333333333333333==========");
                        canskill[i]=false;
                        animator.SetFloat("atkindex", i);
                        animator.GetComponent<bossDash>().duringAtk=true;
                    }
                    else if(i==2)
                    {
                        canskill[i]=false;
                        animator.SetFloat("atkindex", i);   
                        animator.GetComponent<bossDash>().duringAtk=true;
                    } 
                    
                }
            }

            if(!animator.GetComponent<bossDash>().duringAtk)
            {
                if(animator.GetComponent<bossDash>().cansend)
                {
                    if(Vector2.Distance(player.position, rb.position) >=suckRange)
                    {
                        animator.GetComponent<bossDash>().iscircle = false;
                    }
                    else
                    {
                        animator.GetComponent<bossDash>().iscircle = true;
                    }
                    animator.GetComponent<bossDash>().duringAtk = true;
                    animator.SetFloat("sendBite", 1);
                    // animator.GetComponent<bossDash>().suckM = true;
                    animator.GetComponent<bossDash>().cansend = false;
                }
                else if(Vector2.Distance(player.position, rb.position) <=suckRange&&animator.GetComponent<bossDash>().cansuck)
                {
                    animator.GetComponent<bossDash>().duringAtk = true;
                    animator.SetFloat("suckB", 1);
                    animator.GetComponent<bossDash>().cansuck = false;
                }
            }

            
        }

        // else if(animator.GetComponent<bossDash>().cansend)
        // {
        //     if(Vector2.Distance(player.position, rb.position) >=suckRange)
        //     {
        //         animator.GetComponent<bossDash>().iscircle = false;
        //     }
        //     else
        //     {
        //         animator.GetComponent<bossDash>().iscircle = true;
        //     }
        //     animator.GetComponent<bossDash>().duringAtk = true;
        //     animator.SetFloat("sendBite", 1);
        //     // animator.GetComponent<bossDash>().suckM = true;
        //     animator.GetComponent<bossDash>().cansend = false;
        // }
        // else if(Vector2.Distance(player.position, rb.position) <=suckRange)
        // {
        //     // animator.GetComponent<bossDash>().duringAtk = true;
        //     // animator.SetFloat("suckB", 1);
        //     animator.GetComponent<bossDash>().cansuck = false;
        // }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      animator.SetFloat("DashAtk", 0);
      animator.SetFloat("sendBite", 0);
      animator.SetFloat("suckB", 0);
      animator.SetFloat("atkindex", -1);
    }

}
