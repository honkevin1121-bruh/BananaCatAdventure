using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class spin : MonoBehaviour
{
    public Transform pT;

    public float speed;
    public float r;
    public float offset;
    float angle; 
    float Rangle; 
    public LayerMask enemylayers;
    cam c;

    float Times;
    public int kilcount;
    int lll;

    public watcherAtk wh;
    private void Start() {
        kilcount = 0;
        GameObject Camera = GameObject.Find("Main Camera");
        c = Camera.GetComponent<cam>();
        //pT = GameObject.Find("player").GetComponent<Transform>();
        spinnerScript Daddy = GetComponentInParent<spinnerScript>();
        speed = Daddy.speed;
        //r = Daddy.r;
        Times = Daddy.times;
        lll = Daddy.ActivateShieldLvL;
        if(GameObject.Find("watcher")!=null)
        {
            wh = GameObject.Find("watcher").GetComponent<watcherAtk>();
        }
    }
    private void Update() {

        if(pT.gameObject.name=="player")
        {
            float RPS = speed * Mathf.Deg2Rad;
            
            Rangle = speed*Time.deltaTime;
            transform.Rotate(Vector3.up,Rangle);
            angle += RPS*Time.deltaTime;

            transform.position = new Vector3(pT.position.x + r*Mathf.Cos(angle - offset* Mathf.Deg2Rad) , pT.position.y + r*Mathf.Sin(angle - offset* Mathf.Deg2Rad), transform.position.z);

            if(angle >= 2*Mathf.PI)
            {
                angle -= 2*Mathf.PI;
            }
        }
        else if(wh!=null)
        {
            if(!wh.isfreeze)
            {
                float RPS = speed * Mathf.Deg2Rad;
                
                Rangle = speed*Time.deltaTime;
                transform.Rotate(Vector3.up,Rangle);
                angle += RPS*Time.deltaTime;

                transform.position = new Vector3(pT.position.x + r*Mathf.Cos(angle - offset* Mathf.Deg2Rad) , pT.position.y + r*Mathf.Sin(angle - offset* Mathf.Deg2Rad), transform.position.z);

                if(angle >= 2*Mathf.PI)
                {
                    angle -= 2*Mathf.PI;
                }
            }
        }
    }


private void OnTriggerEnter2D(Collider2D other) {
    if(pT.gameObject.name =="player")
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 1.5f, enemylayers);

		
        if(hitEnemies.Length > 0)
		{
            kilcount += 1;
			c.Shake();
            if(lll < 3)
            {
                if(kilcount > 1)
                {
                  Destroy(gameObject);
                }
            }
            else
            {
                if(kilcount > 2)
                {
                    Destroy(gameObject);
                    // if(transform.childCount<=0)
                    // {
                        
                    // }
                }
            }


		}
        

		foreach(Collider2D enemy in hitEnemies)
		{
			Transform GT = enemy.transform;
			Debug.Log(GT);
        	Transform CT = GT.GetChild(0);
			CT.gameObject.GetComponent<enemy>().TakeDamage(pT.gameObject.GetComponent<playerProperties>().atkStrength*Times);
			pT.gameObject.GetComponent<p_combat>().KnockbackEnemy(GT,gameObject.transform,Times);

		}
    }
    else
    {
       if(other.tag == "Player")
        {
            other.transform.GetComponent<Playerhealth>().TakeDamage(pT.GetComponent<watcherAtk>().whStrength);
        }
    }



        
}
}
