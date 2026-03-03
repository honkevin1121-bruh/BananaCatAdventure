using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossDash : MonoBehaviour
{
    Rigidbody2D rb;
    Transform player;
    Vector3 ppos;
    LineRenderer lineRenderer;
    public LayerMask BMask;
    public bool StartCount;
    public float T;
    public float duration;
    public float Ssec;
    public float Lsec;
    float sec;
    public float Spercentage;
    public float Lpercentage;
    float percentage;
    public float pp;
    public float r;
    public float dashStrength;
    public bool attking;
    public float atkInterval;
    public List<GameObject> collidingObjects = new List<GameObject>();
    public int Mouthnum;
    public bool cansend;
    public float sendCD;
    public float sendTimer;
    Material myMaterial;

    //public bool isAtking;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        rb = GetComponent<Rigidbody2D>();
        StartCount = false;
        duration = sec/percentage;
        atkInterval = 0.25f;   
        myMaterial = new Material(Shader.Find("Sprites/Default"));
    }


    public void ShortDash()
    {
        T = 0;
        percentage = Spercentage;
        sec = Ssec;
        duration = sec/percentage;
        ppos = player.position;
        StartCount = true;
        attking = true;


    }

    public void LongDash()
    {
        T = 0;
        sec = Lsec;
        percentage = Lpercentage;
        duration = sec/percentage;
        StartCount = true;
        attking = true;
    }

    IEnumerator fly()
    {
        isfly =true;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1.33f);
        //transform.position = player.position;
        isfly = false;
    }
    public bool isfly;
    public LayerMask Pmask;
    public float atkrange;
    IEnumerator Land()
    {
        Debug.Log("{{{{{{{{{{{{{{{{{{{{{}}}}}}}}}}}}}}}}}}}}}");
        GameObject.Find("Main Camera").GetComponent<cam>().Shake();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, atkrange, Pmask);

		if(hitEnemies.Length > 0)
		{
            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.transform.GetComponent<Playerhealth>().TakeDamage(dashStrength*1.5f);
            }
        }
        // yield return new WaitForSeconds(0.5f);
        GetComponent<Collider2D>().enabled =true;
        duringAtk = false;
        yield return null;
    }

    public int n;
    public float spinShTime;
    public GameObject BU;
    IEnumerator PreparespiningShoot()
    {
        duringAtk = true;
        int count = n;
        float theta = 360/n;
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
        yield return new WaitForSeconds(sendBiteDtime);
        lineRenderer.positionCount = 0;
        
        
        for(int i = 0; i<count; i++)
        {
            Direc = Quaternion.Euler(0, 0, i*theta)*(ppos - transform.position).normalized;
            StartCoroutine(spiningShoot(Direc));
        }
        // Direc = Quaternion.Euler(0, 0, 0)*(ppos - transform.position).normalized;
        // StartCoroutine(spiningShoot(Direc));

        yield return new WaitForSeconds(spinShTime);
        duringAtk = false;

    }
    // public float spinShTimer=0;

    IEnumerator spiningShoot(Vector3 SDirec)
    {
        float spinShTimer=0;
        while(spinShTimer<spinShTime)
        {
            Vector3 direc = Quaternion.Euler(0, 0, 180f*spinShTimer/spinShTime)*SDirec.normalized;
            GameObject bu = Instantiate(BU, transform.position+direc*9f, Quaternion.identity);
            bu.SetActive(true);
            bu.GetComponent<Bulletsys>().friend = false;
            bu.GetComponent<Bulletsys>().tp = transform.position+direc*10f;
            bu.GetComponent<Bulletsys>().sp = transform.position+direc*9f;
            bu.GetComponent<Bulletsys>().maxDistance = 30f;
            bu.GetComponent<Bulletsys>().BDmg = dashStrength*0.5f;
            yield return new WaitForSeconds(0.075f);
            spinShTimer+=Time.deltaTime+0.075f;
        }
    }

    public void PrepareDash()
    {
        ppos = player.position;
        Vector3 direc = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direc, 500, BMask);
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = myMaterial;
        if(hit)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }
    }

    public bool cansendMou;
    public float sendMouDtime;
    public GameObject MU;
    public bool suckM;
    IEnumerator PrepareSendMou()
    {
        
        // Debug.Log("--------------------------");
        ppos = player.position;
        Vector3 endpos;
        Vector3 direc = player.position - transform.position;
        Quaternion rotation = Quaternion.Euler(0, 0, lilDegree);
        Vector3 DIrec = rotation * direc;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, DIrec.normalized, 500, BMask);
        endpos = hit ? (Vector3)hit.point : transform.position + DIrec*500;
        spos = transform.position + DIrec;
        Debug.Log(spos+"ppppppppppppppppppppppppp");
        CreateRay(transform.position,endpos,sendMouDtime);
        yield return new WaitForSeconds(sendMouDtime);
        
        GameObject bu = Instantiate(MU, transform.position+(endpos - transform.position).normalized*9f, Quaternion.identity);
            bu.GetComponent<mouBomb>().Streng = dashStrength;
            bu.GetComponent<mouBomb>().tp = spos;
            bu.GetComponent<mouBomb>().sp = transform.position;
            bu.SetActive(true);
            if(suckM)
            {
                bu.GetComponent<mouBomb>().issuck = true;
            }

            duringAtk=false;

    }



    public bool iscircle;
    public void Send()
    {
        if(iscircle)
        {
            StartCoroutine(PrepareSendCirB());
        }
        else
        {
            StartCoroutine(PrepareSendB1());
        }
    }

    public float lilDegree;
    public Vector3 spos;
    public float sendBiteDtime;
    IEnumerator PrepareSendB1()
    {
        // if(angry)
        // {
        //     Dtime1 = 7f/60f;
        // }
        // Debug.Log("prepare");
        ppos = player.position;
        Vector3 endpos;
        Vector3 direc = player.position - transform.position;
        Quaternion rotation = Quaternion.Euler(0, 0, lilDegree);
        Vector3 DIrec = rotation * direc;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, DIrec.normalized, 500, BMask);
        endpos = hit ? (Vector3)hit.point : transform.position + DIrec*500;
        spos = transform.position + DIrec;
        
        CreateRay(transform.position,endpos,sendBiteDtime*0.6f);
        yield return new WaitForSeconds(sendBiteDtime);
        StartCoroutine(ReleaseBite(transform.position,endpos,false));
    }


    void CreateRay(Vector3 start, Vector3 end,float LifeT)
    {
        GameObject go = new GameObject("Ray");
        go.transform.SetParent(this.transform);
        LineRenderer lr = go.AddComponent<LineRenderer>();
        raydel rd= go.AddComponent<raydel>();
        rd.dura = LifeT;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.material = myMaterial;
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    IEnumerator PrepareSendCirB()
    {
        // if(angry)
        // {
        //     Dtime1 = 7f/60f;
        // }
        // Debug.Log("prepare");
        ppos = player.position;
        
        CreateRingRay(transform.position,ppos,sendBiteDtime*0.6f,Vector2.Distance(player.position,transform.position));
        yield return new WaitForSeconds(sendBiteDtime);
        StartCoroutine(ReleaseBite(transform.position,ppos,true));
    }
    
    int Num = 100;
    void CreateRingRay(Vector3 start, Vector3 end,float LifeT,float R)
    {
        GameObject go = new GameObject("Ray");
        go.transform.SetParent(this.transform);
        LineRenderer lr = go.AddComponent<LineRenderer>();
        raydel rd= go.AddComponent<raydel>();
        rd.dura = LifeT;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.material = myMaterial;
        lr.positionCount = Num+1;
        //lr.useWorldSpace = false; 
        // lr.loop = true;
        float ang=0;
        for(int i=0;i<Num+1;i++)
        {
            float x = Mathf.Cos(ang*Mathf.Deg2Rad)*R;
            float y = Mathf.Sin(ang*Mathf.Deg2Rad)*R;
            lr.SetPosition(i,transform.position+new Vector3(x,y,0));
            ang+=360f/Num;
        }
    }

    public GameObject MOU;
    public float alpha;
    IEnumerator ReleaseBite(Vector3 startp,Vector3 endp,bool iscir)
    {
        float ang=0;
        Vector3 dire = (endp - startp).normalized;
        if(iscir)
        {
            for(int i = 0;i<Mouthnum;i++)
            {
                Vector3 Direc = new Vector3(Mathf.Cos(Mathf.Deg2Rad*ang)*Vector2.Distance(endp,startp),Mathf.Sin(Mathf.Deg2Rad*ang)*Vector2.Distance(endp,startp),0);
                GameObject mou = Instantiate(MOU, transform.position+Direc, Quaternion.identity);  
                mou.transform.GetChild(0).GetComponent<bite>().biteStrength = dashStrength;
                mou.gameObject.SetActive(true);
                ang+=360/Mouthnum;
                yield return null;
            }
            duringAtk =false;
        }
        else
        {
            for(int i = 0;i<Mouthnum;i++)
            {
                GameObject mou = Instantiate(MOU, transform.position+dire*9f+i*dire*alpha, Quaternion.identity);  
                mou.transform.GetChild(0).GetComponent<bite>().biteStrength = dashStrength;
                mou.gameObject.SetActive(true);
                // yield return new WaitForSeconds(0.005f);
                yield return null;
            }
            duringAtk = false;
        }
        
        
    }
    public float pullStrength; // 最大拉力
    public float pullRadius;    // 吸引半徑
    public float sucksec;
    public bool cansuck;
    public float suckCD;
    public float suckTimer;
    IEnumerator Suck()
    {
        sucking = true;
        yield return new WaitForSeconds(sucksec);
        sucking = false;
        duringAtk =false;
    }
    
    public float DDD;
    public bool sucking;
    public bool duringAtk;


    public float KBTimer=0;
    public float keepBiteDura;

    public List<float> skilltimer;
    public List<float> skillCD;
    public List<bool> canskill;
    private void Update() {

        if(keepBiteDura>KBTimer&&GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("keepbite"))
        {
            KBTimer += Time.deltaTime;
        }
        else if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("keepbite"))
        {
            KBTimer = 0;
            GetComponent<Animator>().SetFloat("endbite",1);
        }

        if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("endBIte"))
        {

            if(GetComponent<Animator>().GetFloat("endbite")>0)
            {
                GetComponent<Animator>().SetFloat("endbite",0);
                duringAtk=false;
            }
            
        }

        if(sucking)
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
        }

        if(sendTimer<sendCD&&!cansend&&!duringAtk)
        {
            sendTimer+=Time.deltaTime;
        }
        else if(sendTimer>=sendCD&&!cansend)
        {
            sendTimer = 0;
            cansend = true;
        }

        if(suckTimer<suckCD&&!cansuck&&!duringAtk)
        {
            suckTimer+=Time.deltaTime;
        }
        else if(suckTimer>=suckCD&&!cansuck)
        {
            suckTimer = 0;
            cansuck = true;
        }

        for(int i=0;i<4&&GetComponent<bossHealth>().isPhase2;i++)
        {
            if(skilltimer[i]<skillCD[i]&&!canskill[i]&&!duringAtk)
            {
                skilltimer[i]+=Time.deltaTime;
            }
            else if(skilltimer[i]>=skillCD[i]&&!canskill[i])
            {
                skilltimer[i] = 0;
                canskill[i] = true;
            }
        }
        
        r = Vector2.Distance(player.position, rb.position);
        if(StartCount)
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
                StartCount = false;
                lineRenderer.positionCount = 0;
            }
        }

        if(!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("shortDash") && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("longDash"))
        {
            // attking = false;
        }

        if(Input.GetKey(KeyCode.W))
        {
            lilDegree = -DDD*Mathf.Sign(transform.position.x - player.position.x);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            lilDegree = DDD*Mathf.Sign(transform.position.x - player.position.x);            
        }
        else
        {
            lilDegree = 0;
        }


        /*
        if(Input.GetKeyDown(KeyCode.V))
        {
           ShortDash();
        }
        */

        // if(Vector2.Distance(player.position, transform.position) <= 15 && 10 < Vector2.Distance(player.position, transform.position))
        // {
        //     GetComponent<Animator>().SetFloat("DashAtk",1);
        // }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherObject = collision.gameObject;
        if (!collidingObjects.Contains(otherObject) && otherObject.tag == "barrier")
        {
            collidingObjects.Add(otherObject);
        }
    }

        private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject otherObject = collision.gameObject;
        if (collidingObjects.Contains(otherObject) && otherObject.tag == "barrier")
        {
            collidingObjects.Remove(otherObject);
        }
    }
    

    
}
