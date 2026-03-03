using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterAttack : MonoBehaviour
{
    public bool Count;
    public float T;
    public float duration;
    public float sec;
    public float AtkRange;
    public LayerMask PMask;
    public float spikeStrength;
    public float percentage;

    Transform player;
    Rigidbody2D rb;
    Vector3 ppos;
    float r;
    float pp;
    cam camera;

    public float shootTime;
    public float lastTime;
    public bool canshooot;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        Count = false;
        duration = sec/percentage;   
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cam>();
        spikeStrength = 5;
    }

    public void Fly()
    {
        T = 0;
        duration = sec/percentage;
        Count = true;
        ppos = player.position;
    }

    public void CloseATK()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(rb.position, AtkRange, PMask);

        if(colInfo != null)
        {
            camera.Shake();
            colInfo.GetComponent<Playerhealth>().TakeDamage(spikeStrength);
        }
    }

    void OnDrawGizmosSelected()
    {
	    Gizmos.DrawWireSphere(transform.position, AtkRange);
    }



    // Update is called once per frame
    void Update()
    {
        r = Vector2.Distance(player.position, rb.position);
        if(Count)
        {
            
            if(T < duration*percentage)
            {
                T+= Time.deltaTime;
                Vector3 newpos = Vector3.Lerp(transform.position, ppos, T/duration);
                transform.position = newpos;
                //rb.MovePosition(newpos);
                pp = T/duration;
            }
            else
            {
                Count = false;
            }
        }

        if(lastTime > 0 && !canshooot){
            lastTime -= Time.deltaTime;
        }
        else
        {
            canshooot = true;
            lastTime = shootTime;
        }
    }
}
