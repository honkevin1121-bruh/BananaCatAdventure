using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_movement : MonoBehaviour
{
    public float movespeed;
    private Rigidbody2D rb;
    public Vector2 moveDir;
    public SpriteRenderer spr;
    public bool isAtking;
    public Animator PAni;

    public Slider Pslider;

    public Vector2 externalVelocity; // 儲存外力的速度
    public float resistanceDecay = 5f; // 外力消失的速度（摩擦力）
    void Start()
    {
        movespeed = GetComponent<playerProperties>().speed;
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();   
    }

    void Update()
    {

            // if(moveDir == Vector2.zero)
            // {
            //     rb.mass = 1000000f;
            // }
            // else
            // {
            //     rb.mass = 1f;
            // }

        Vector2 inputDir = Vector2.zero;

        if(!isAtking && Time.timeScale == 1f)
        {
            
            if(Input.GetKey(KeyCode.W)) inputDir.y = 1;
            if(Input.GetKey(KeyCode.S)) inputDir.y = -1;
            if(Input.GetKey(KeyCode.D)) { inputDir.x = 1; spr.flipX = true; }
            if(Input.GetKey(KeyCode.A)) { inputDir.x = -1; spr.flipX = false; }
            
            // 規範化方向並乘以速度，避免斜向過快
            inputDir = inputDir.normalized * movespeed;
            
            // 設定動畫狀態
            PAni.SetFloat("status", inputDir.magnitude > 0 ? 1f : 0f);

            externalVelocity = Vector2.Lerp(externalVelocity, Vector2.zero, resistanceDecay * Time.deltaTime);

            // 3. 關鍵：疊加兩者！
            // 玩家的 velocity 是「想走的方向」加上「被推的方向」
            rb.velocity = inputDir + externalVelocity;

            //     if(Input.GetKey(KeyCode.W))
            //     {
            //         PAni.SetFloat("status", 1f);
            //         moveDir.y = movespeed;
            //     }

            //     if(Input.GetKey(KeyCode.D))
            //     {
            //         PAni.SetFloat("status", 1f);
            //         spr.flipX = true;
            //         moveDir.x = movespeed;
            //     }

            //     if(Input.GetKey(KeyCode.A))
            //     {
            //         PAni.SetFloat("status", 1f);
            //         spr.flipX = false;
            //         moveDir.x = -movespeed;
            //     }

            //     if(Input.GetKey(KeyCode.S))
            //     {
            //         PAni.SetFloat("status", 1f);
            //         moveDir.y = -movespeed;
            //     }

            //     if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) )
            //     {
            //         PAni.SetFloat("status", 0f);
            //         moveDir = Vector2.zero;
            //     }
            // }
            // else
            // {   
            //     PAni.SetFloat("status", 0f);
            //     moveDir = Vector2.zero;

        // }


        // rb.velocity = moveDir;

        }

    }

    public void AddKnockback(Vector2 force)
    {
        //  Debug.Log("777777777777777777777777777");    
        // 直接累加外力速度，而不是 AddForce，這樣手感最精確
        externalVelocity += force;
    }

    private void LateUpdate() {
        Vector3 targetposition = gameObject.transform.position;
        targetposition.y += 1f;
        Pslider.transform.position = Camera.main.WorldToScreenPoint(targetposition);
    }

    
}
