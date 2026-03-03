using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watcherRun : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed; 
    public float LongFlyRange;

    public float R; 
    // public float rayRange;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();   
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!animator.GetComponent<watcherAtk>().isfreeze)
        {
            if(!animator.GetComponent<watcherAtk>().Spinn.activeSelf)
            {
                // speed = 8;
            }

            Vector2 playerPos = player.position;
            Vector2 currentPos = rb.position;
            
            // 計算方向與距離
            Vector2 offset = currentPos - playerPos;
            float dist = offset.magnitude;
            
            // 計算目標位置 (距離玩家 R 的那個點)
            Vector2 targetPos = playerPos + (offset / dist) * R;
            
            // 如果已經很接近目標點，就不要移動（防止微小震動）
            if (dist > 0.001f) {
                Vector2 nextPos = Vector2.MoveTowards(currentPos, targetPos, speed * Time.fixedDeltaTime);
                rb.MovePosition(nextPos);
            }
        }
        // float PB = Vector2.Distance(player.position, rb.position);
        // Vector3 Tpos = (R*rb.position - (Vector2)player.position*(R-PB))/PB;
        // Vector2 newpos = Vector2.SmoothDamp(rb.position, Tpos, speed*Time.fixedDeltaTime);
        // rb.MovePosition(newpos);
        

        if(Vector2.Distance(player.position, rb.position) >= LongFlyRange&&!animator.GetComponent<watcherAtk>().isfreeze)
        {
            animator.SetFloat("Fly", 1);
            
        }
        else if(animator.GetComponent<watcherAtk>().canray&&Vector2.Distance(player.position, rb.position) >= animator.GetComponent<watcherAtk>().protectRange)
        {
            if(!animator.GetComponent<watcherAtk>().isfreeze)
            {
                animator.SetFloat("rayatk", 1);
                animator.GetComponent<watcherAtk>().canray = false;
            }
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

      animator.SetFloat("Fly", 0);
      //animator.SetFloat("protect", 0);
      animator.SetFloat("rayatk", 0);
    }
}
