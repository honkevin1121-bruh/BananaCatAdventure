using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicExpand : MonoBehaviour
{
    public float s;
    public float DT;
    public float t;
    public float mcForce;
    // Start is called before the first frame update
    void Start()
    {

        //s =1;
        t = 0f;
        if(transform.parent.gameObject.tag == "Player")
        {
            mcForce = 0.5f;
        }
        
    }

    void OnEnable()
    {
        t=0;
        GetComponent<Collider2D>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(t<DT)
        {
            t += Time.deltaTime*s;
            transform.localScale = new Vector3 (30f*t/DT,30f*t/DT,0f);
        }
        else
        {
            transform.localScale = new Vector3 (0,0,0);
            t = 0;
            if(transform.parent.gameObject.tag != "Player")
            {
                transform.parent.GetComponent<watcherAtk>().Ftimer = 0;
                gameObject.SetActive(false);
            }

            if(transform.parent.gameObject.tag == "Player"&&!isTS)
            {
                transform.parent.GetComponent<playerProperties>().CountList[1] = true;
                gameObject.SetActive(false);
            }
            else if(isTS)
            {
                transform.parent.parent.GetComponent<playerProperties>().CountList[4] = true;
                transform.parent.gameObject.SetActive(false);
            }
            GetComponent<Collider2D>().enabled = true;
            
        }
    }

    public bool isTS;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "enemy"&&transform.parent.gameObject.tag == "Player")
        {
            if(other.transform.GetChild(0).GetComponent<enemy>() != null)
            {
                if(!isTS)
                {
                    Transform GT = other.transform;
                    Transform CT = GT.GetChild(0);
                    //Debug.Log("sssssssssssssssssssssssss00000000sssssssssssss");
                    p_combat pcom =  transform.parent.GetComponent<p_combat>();
                    CT.gameObject.GetComponent<enemy>().TakeDamage(0f);
                    pcom.KnockbackEnemy(GT,transform.parent,pcom.DamageStrength*mcForce);
                }
                else
                {
                    Debug.Log("sssssssssssssssssssssssss00000000sssssssssssss");
                    Transform GT = other.transform;
                    Transform CT = GT.GetChild(0);
                    CT.gameObject.GetComponent<enemy>().StopDura = 2.5f;
                    CT.gameObject.GetComponent<enemy>().stop = true;
                }
                
            }
        }
        else if(transform.parent.gameObject.tag != "Player"&&other.tag == "Player")
        {
            GetComponent<Collider2D>().enabled = false;
             //Debug.Log("0000000000000000");  
            player_movement pm = other.transform.GetComponent<player_movement>();
            pm.AddKnockback((Vector2)(other.transform.position-transform.parent.position).normalized*mcForce);
        }
        else if(other.tag == "bullet"&&transform.parent.gameObject.tag == "Player")
        {
            if(!other.GetComponent<Bulletsys>().friend)
            {
                Destroy(other.gameObject);
            }
        }
        else if(other.tag == "boss"&&transform.parent.gameObject.tag == "Player")
        {
            p_combat pcom =  transform.parent.GetComponent<p_combat>();
            other.gameObject.GetComponent<bossHealth>().BTakeDamage(0f);
            pcom.KnockbackEnemy(other.transform ,transform.parent,pcom.DamageStrength*bossMF);
            GetComponent<Collider2D>().enabled= false;
        }
    }
    public float bossMF;
}
