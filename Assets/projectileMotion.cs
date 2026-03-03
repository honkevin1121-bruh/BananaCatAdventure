using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMotion : MonoBehaviour
{

    Transform player;
    public Vector2 p;
    public Vector3 startPoint;
    public Vector3 controlPoint;
    public Vector3 endPoint;
    public float timer = 0;
    public float duration;
    public LayerMask PMask;
    public float Range;
    public float BStrength;

    Rigidbody2D rb;
    /*
    public float Initialspeed; 
    public float offset;
    public Vector2 direc;
    public float alpha;
    public float theta;
    public float g;
    */
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(fly());
        /*
        Physics2D.gravity = new Vector2(0, g);
        rb = GetComponent<Rigidbody2D>();  
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //p = player.position;
        float dx = Mathf.Abs(player.position.x - transform.position.x);
        float dy = player.position.y - transform.position.y;
        float k = dy/Mathf.Sqrt(dx*dx+dy*dy);
        float NorP = (p.x - transform.position.x)/dx;
        if(k == 1)
        {
            alpha = 90 - 0.0001f;
        }
        else if(k > 0)
        {
            alpha = Mathf.Asin(k) * Mathf.Rad2Deg;
        }
        else
        {
            
            alpha = 0;
        }
        theta = (90+alpha)/2*Mathf.PI/180;
        direc = new Vector2(NorP*Mathf.Cos(theta),Mathf.Sin(theta));
        float sin = Mathf.Sin(theta*2);
        float cos = Mathf.Cos(theta);

        Initialspeed = Mathf.Sqrt(-dx*dx*Physics2D.gravity.y/(dx*sin-2*dy*cos*cos));
        Vector2 force = direc * Initialspeed * rb.mass;
        rb.AddForce(force, ForceMode2D.Impulse);
        */
    }

    IEnumerator fly()
    {
        while (timer < duration) {
        float t = timer / duration;
        
        // 核心公式：二次貝茲曲線
        // Vector3.Lerp(A, B, t) 是線性插值
        Vector3 m1 = Vector3.Lerp(startPoint, controlPoint, t);
        Vector3 m2 = Vector3.Lerp(controlPoint, endPoint, t);
        transform.position = Vector3.Lerp(m1, m2, t);

        timer += Time.deltaTime;
        yield return null;
        }
        transform.position = endPoint;
        Collider2D colInfo = Physics2D.OverlapCircle(transform.position, Range, PMask);

            if(colInfo != null)
            {
            Debug.Log("ffffffffffff0000000000000000fffff");
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cam>().Shake();
                colInfo.GetComponent<Playerhealth>().TakeDamage(BStrength);
            }
        
        Destroy(gameObject);
    }

    public void Set(Vector3 StartP, Vector3 HighP,Vector3 EndP,float duraT,float Bstren)
    {
        startPoint = StartP;
        controlPoint = HighP;
        endPoint = EndP;
        BStrength = Bstren; 
        //Debug.Log("ppppppppppppppppppppppppp"+endPoint);
        duration = duraT;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cam>().Shake();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Playerhealth>().TakeDamage(BStrength);
            Destroy(gameObject);
        }
        else if(other.tag == "barrier")
        {
            Destroy(gameObject);
        }
    }
/*
    private void Update() {
        if(transform.position.x < p.x +offset && transform.position.x >= p.x -offset && transform.position.y < p.y +offset && transform.position.y >= p.y -offset)
        {
            Destroy(gameObject);
        }
    }
*/

}
