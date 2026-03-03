using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletsys : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float maxDistance;
    public bool friend;
    public Vector3 tp;
    public Vector3 sp;

    //public Vector3 D;
    public float BDmg;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();  
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        //D = tp - sp;
        // 1. 讓物件朝設定的方向移動
        transform.Translate((tp-sp).normalized * speed * Time.deltaTime);

        // 2. 計算目前位置與起點的距離
        float traveledDistance = Vector3.Distance(sp, transform.position);

        // 3. 如果超過設定距離，就銷毀物件
        if (traveledDistance >= maxDistance)
        {
            Destroy(gameObject);
        }


    }

    private void OnTriggerEnter2D(Collider2D other) {
    if(other.tag == "Player" && !friend)
    {
        Debug.Log("0000000000000000000000000000000000");
        Destroy(gameObject);
        player.GetComponent<Playerhealth>().TakeDamage(BDmg);
    }
    else if(other.tag == "barrier")
    {
        Debug.Log("11111111111111111111111111111");
        Destroy(gameObject);
    }
    else if(other.tag == "enemy" && friend)
    {
        Debug.Log("0000000000000000000000000000000000");
        if(other.transform.GetChild(0).GetComponent<enemy>() != null)
        {
            Transform GT = other.transform;
        	Transform CT = GT.GetChild(0);
            //Debug.Log("sssssssssssssssssssssssss00000000sssssssssssss");
            p_combat pcom =  player.GetComponent<p_combat>();
			CT.gameObject.GetComponent<enemy>().TakeDamage(pcom.DamageStrength*BDmg);
            pcom.KnockbackEnemy(GT,player,pcom.DamageStrength);
            Destroy(gameObject);
            
        }
    }
    }
}
