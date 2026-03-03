using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watcherAtk : MonoBehaviour
{
    public bool Count;
    public float T;
    public float duration;
    public float sec;
    // public LayerMask PMask;
    public float whStrength;
    public float percentage;

    Transform player;
    Rigidbody2D rb;
    Vector3 ppos;
    float r;
    float pp;
    cam camera;

    public float rayTime;
    public float lastTime;
    public bool canray;
    public bool isprotected;
    public int shieldnum;
    public bool isfreeze;

    public GameObject Spinn;
    public GameObject RAY;
    public float protectRange;
    public float boxWidth;
    public float boxHeight;
    public float lilDegree;
    public float Dtime;
    public int n;

    Vector3 spos;
    LineRenderer lineRenderer;
    Material myMaterial;

    public LayerMask BMask;
    public bool notray=true;

    public float Fdura;
    public float Ftimer;


    // Start is called before the first frame update
    void Start()
    {
        // lastTime = rayTime;
        // notray = true;
        lineRenderer = GetComponent<LineRenderer>(); 
        lineRenderer.startWidth = 0.02f;   
        lineRenderer.endWidth = 0.02f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        myMaterial = lineRenderer.material;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        Count = false;
        duration = sec/percentage;   
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cam>();
        // whStrength = 5;
        Spinn.GetComponent<spinnerScript>().spinAxis = gameObject.transform;
        //shieldnum = 3;
        // CS();

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
        if(Spinn.transform.childCount>0)
        {
            Spinn.GetComponent<spinnerScript>().BossCloseSh();
        }
    }

    public void Safe()
    {
        if(Spinn.transform.childCount>0)
        {
            Spinn.GetComponent<spinnerScript>().BossSafe();
        }
    }

    void OnDrawGizmosSelected()
    {
	    Gizmos.DrawWireSphere(transform.position, protectRange);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        // 設定 Gizmos 矩陣以符合物體的旋轉與位置
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

        // 在局部空間中，中心點在 X 軸偏移一半，尺寸為 (boxWidth, boxHeight)
        Vector3 localCenter = new Vector3(boxWidth / 2f, 0, 0);
        Vector3 fullSize = new Vector3(boxWidth, boxHeight, 0);

        Gizmos.DrawWireCube(localCenter, fullSize);
    }

    public void CS()
    {
        // Debug.Log("f4444444444444444444444444444444");
        if(shieldnum !=0)
        {
            Debug.Log("f4444444444444444444444444444444");
            Spinn.GetComponent<spinnerScript>().BossShieldActivated(shieldnum);
        }
        else
        {
            Spinn.GetComponent<spinnerScript>().killkids();
        }
        
    }

    public void freeze(float sec)
    {
        StartCoroutine(StartFreeeze(sec));
        Debug.Log("freeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeze");
    }

    IEnumerator StartFreeeze(float Ft)
    {
        transform.GetChild(1).GetComponent<spikespin>().Rsped = 0;
        notray = false;
        isfreeze = true;
        yield return new WaitForSeconds(Ft);
        notray = true;
        isfreeze = false;
        transform.GetChild(1).GetComponent<spikespin>().Rsped = 200;
    }

    public void ReleaseRay(bool locked)
    {
        GameObject ray = Instantiate(RAY, transform.position, Quaternion.identity);
        raySplash rayS = ray.GetComponent<raySplash>();
        rayS.direction = spos - transform.position;
        rayS.wide = boxWidth;
        rayS.length = boxHeight;
        rayS.Raystren = whStrength;
        ray.SetActive(true);
        rayS.islocked =locked;
        rayS.Startstrike();
    }

        IEnumerator PrepareRay1()
    {
        bool islock=false;
        if(canlockray)
        {
            lockRayTimer=0;
            canlockray=false;
            islock=true;
        }
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
        
        // if(!angry)
        // {
        //     CreateRay(transform.position,endpos,Dtime1);
        // }
        // else
        // {
        //     CreateRay(transform.position,endpos,Dtime1*0.6f*DelayP);
        // }
        CreateRay(transform.position,endpos,Dtime*0.6f);
        yield return new WaitForSeconds(Dtime);
        ReleaseRay(islock);
    }

    IEnumerator PrepareRaySpin()
    {
        // bool islock=false;
        // if(canlockray)
        // {
        //     lockRayTimer=0;
        //     canlockray=false;
        //     islock=true;
        // }
        ppos = player.position;
        Vector3 endpos;
        Vector3 direc = player.position - transform.position;
        Quaternion rotation = Quaternion.Euler(0, 0, lilDegree);
        Vector3 DIrec = rotation * direc;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, DIrec.normalized, 500, BMask);
        endpos = hit ? (Vector3)hit.point : transform.position + DIrec*500;
        spos = transform.position + DIrec;
        CreateRay(transform.position,endpos,Dtime*0.6f);
        yield return new WaitForSeconds(Dtime);
        GameObject ray = Instantiate(RAY, transform.position, Quaternion.identity);
        raySplash rayS = ray.GetComponent<raySplash>();
        rayS.direction = spos - transform.position;
        rayS.pos = (Vector2)transform.position;
        rayS.Raystren = whStrength;
        ray.SetActive(true);
        // rayS.islocked = islock;
        rayS.StartSpinstrike(1);
    }

    IEnumerator PrepareMutiRaySpin()
    {
        int count = n;
        float theta = 360/n;
        Debug.Log("prepare333");
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
            yield return new WaitForSeconds(Dtime);
            lineRenderer.positionCount = 0;

        for(int i = 0; i<count; i++)
        {
            Direc = Quaternion.Euler(0, 0, i*theta)*(ppos - transform.position).normalized;
            spos = transform.position + Direc;
            GameObject ray = Instantiate(RAY, transform.position, Quaternion.identity);
            raySplash rayS = ray.GetComponent<raySplash>();
            rayS.direction = spos - transform.position;
            rayS.pos = (Vector2)transform.position;
            rayS.Raystren = whStrength;
            ray.SetActive(true);
            rayS.StartSpinstrike(2);
        }

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

    public float DDD;

    public void SelectRay()
    {
        // Debug.Log(canspinray&&notray);
        if(canspinray)
        {
            Debug.Log("pppppppppppppppppppppppppppp");
            if(GetComponent<bossHealth>().isPhase2)
            {
                StartCoroutine(PrepareMutiRaySpin());
            }
            else
            {
                StartCoroutine(PrepareRaySpin());
            }
            canspinray=false;
        }
        else
        {
            // Debug.Log(canspinray&&notray);
            StartCoroutine(PrepareRay1());
        }
    }

    public float spinRayTimer;
    public float lockRayTimer;
    public float spinRayCD;
    public float lockRayCD;
    public bool canspinray;
    public bool canlockray;
    // Update is called once per frame
    void Update()
    {

        if(lastTime > 0){
            if(!isfreeze)
            {
                lastTime -= Time.deltaTime;
            }
        }
        else if(notray)
        {
            canray = true;
            notray = false;
        }
        
        if(lockRayCD>lockRayTimer)
        {
            if(!isfreeze)
            {
                lockRayTimer += Time.deltaTime;
            }
        }
        else if(notray)
        {
            canlockray=true;
        }

        if(spinRayCD>spinRayTimer)
        {
            if(!isfreeze)
            {
                spinRayTimer += Time.deltaTime;
            }
        }
        else if(notray)
        {
            canspinray=true;
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


        if(GetComponent<Animator>().GetFloat("phase2")<=0)
        {
            if(shieldnum !=0)
            {
                if(Vector2.Distance(player.position, GetComponent<Rigidbody2D>().position) <= protectRange&&!isfreeze)
                {
                    if(!isprotected)
                    {
                        isprotected = true;
                        CloseATK();
                    }
                }
                else
                {
                    if(isprotected)
                    {
                        Safe();
                    }
                    isprotected = false;
                
                }
            }
            else
            {
                if(Ftimer<Fdura&&!isfreeze)
                {
                    Ftimer+= Time.deltaTime;
                }

                if(Vector2.Distance(player.position, GetComponent<Rigidbody2D>().position) <= protectRange)
                {
                    
                    if(Ftimer>=Fdura&&!isfreeze)
                    {
                        transform.GetChild(0).gameObject.SetActive(true);
                        // transform.GetChild(0).GetComponent<Collider2D>().enabled = true;
                        // Ftimer = 0;
                    }
                }
                
            }

        }
        
    }
}
