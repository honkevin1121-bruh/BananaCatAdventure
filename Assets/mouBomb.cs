using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouBomb : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float maxDistance;
    public Vector3 tp;
    public Vector3 sp;

    //public Vector3 D;
    // public float BDmg;
    Transform player;
    LineRenderer lineRenderer;
    public bossDash FB;
    // Start is called before the first frame update
    void Start()
    {
        FB = GameObject.Find("boss").GetComponent<bossDash>();
        // FB.duringAtk = true;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        isclear = true;
        // Debug.Log(tp-sp);
        rb = GetComponent<Rigidbody2D>();  
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if(issuck)
        {
            GetComponent<CircleCollider2D>().radius = 1f;
        }
        else
        {
            GetComponent<CircleCollider2D>().radius = 0.8f;
        }
    }

    // Update is called once per frame

    public int n;
    public float Dtime;
    public float Streng;
    public GameObject BU;
    public bool isclear;
    Vector3 ppos;
    Vector3 spos;
    Vector3 Direc;
    public LayerMask BMask;

    IEnumerator Prepareshoot3()
    {
        int count = n;
        float theta = 360/n;
        
            yield return new WaitForSeconds(Dtime);
            isclear = true;
            lineRenderer.positionCount = 0; 


        for(int i = 0; i<count; i++)
        {
            Direc = Quaternion.Euler(0, 0, i*theta)*(ppos - transform.position).normalized;
            spos = transform.position + Direc;
            GameObject bu = Instantiate(BU, transform.position, Quaternion.identity);
            bu.SetActive(true);
            bu.GetComponent<Bulletsys>().friend = false;
            bu.GetComponent<Bulletsys>().tp = spos;
            bu.GetComponent<Bulletsys>().sp = transform.position;
            bu.GetComponent<Bulletsys>().BDmg = Streng*0.5f;
        }
        FB.duringAtk = false;
        Destroy(gameObject);
        // gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            if(issuck)
            {
                suck =true;
            }
            else
            {
                isclear = false;
                // Debug.Log("0000000000000000000000000000000000");
                StartCoroutine(Prepareshoot3());
            }
            
        }
    }

    public float pullStrength; // 最大拉力
    public float pullRadius;
    public float Rsped;
    float Rangle;
    public bool suck;
    public bool issuck;
    public float suckTime;
    public float suckDura;
    public int Mouthn;
    public GameObject MOU;
    public bool end;
    public float biteDis;
    float ang=0;
    public float R;
    private void Update() {

        Rangle += Rsped*Time.deltaTime;
        
        transform.GetChild(0).rotation = Quaternion.Euler(0,0,Rangle);

        if(!suck&&!end)
        {
            transform.Translate((tp-sp).normalized * speed * Time.deltaTime);

            // 2. 計算目前位置與起點的距離
            float traveledDistance = Vector3.Distance(sp, transform.position);

            // 3. 如果超過設定距離，就銷毀物件
            if (traveledDistance >= maxDistance)
            {
                FB.duringAtk = false;
                Destroy(gameObject);
            }
        }



        if(!isclear&&!issuck)
        {
            int count = n;
            float theta = 360/n;
            // Debug.Log("prepare333");
            ppos = player.position;
            lineRenderer.positionCount = 2*count;
                Vector3 endpos;
                Vector3 Direc;
                RaycastHit2D hit;
            for(int i = 0; i<count; i++)
            {
                Direc = Quaternion.Euler(0, 0, i*theta)*(ppos - transform.position).normalized;
                hit = Physics2D.Raycast(transform.position, Direc, 500, BMask);
                endpos = hit ? (Vector3)hit.point : transform.position + Direc*500;
                lineRenderer.SetPosition(2*i, transform.position);
                lineRenderer.SetPosition(2*i+1, endpos);
            }
        }

        // Rangle = Rsped*Time.deltaTime;
        // transform.Rotate(0,0,Rangle);

        if(suck)
        {
            player_movement playerM = player.GetComponent<player_movement>();
            if (playerM == null) return;

            // 2. 計算方向：從玩家指向黑洞中心
            Vector2 direction = transform.position - player.position;
            float distance = direction.magnitude;

            // 3. 計算拉力強度：距離越近，拉力越強 (線性衰減)
            float forcePercent = 1f - (distance / pullRadius); // 0~1 之間的比例
            Vector2 finalPull = direction.normalized * forcePercent * pullStrength;

            // 4. 設定玩家的外力速度
            // 注意：這裡我們直接「更新」玩家的 externalVelocity
            playerM.externalVelocity = finalPull;

            if(suckTime<suckDura)
            {
                suckTime+=Time.deltaTime;
            }

            if(distance<=biteDis||suckTime>=suckDura)
            {
                end =true;
                suck = false;
                
                for(int i = 0;i<Mouthn;i++)
                {
                    Debug.Log("========================"+ang);
                    GameObject mou = Instantiate(MOU, transform.position, Quaternion.Euler(0, 0, ang));  
                    // mou.transform.rotation = Quaternion.Euler(0,0,90);
                    Debug.Log("========================"+mou.transform.rotation);
                    mou.transform.GetChild(0).GetComponent<bite>().biteStrength = Streng;
                    mou.transform.GetChild(0).localPosition = new Vector3(0,R,0);
                    mou.gameObject.SetActive(true);
                    ang+=360f/Mouthn;
                }                
                FB.duringAtk = false;
                // Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    
}
