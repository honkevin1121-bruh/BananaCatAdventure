using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class shootThing : MonoBehaviour
{
    //public float shootTime;
    //public float lastTime;
    public GameObject BU;
    public LayerMask BMask;
    public float lilDegree;
    public float DDD;
    public int n;
    public float Dtime1;
    public float Dtime2;
    public float Dtime3;
    public float DelayP;
    LineRenderer lineRenderer;

    Vector3 ppos;
    Vector3 spos;
    public Vector3 Stillpos;

    Transform player;
    public float Streng;
    bool angry;
    Material myMaterial;
    
    void Start()
    {
        angry = gameObject.GetComponent<Animator>().GetBool("angry");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        myMaterial = lineRenderer.material;
        Streng = gameObject.GetComponent<shooterAttack>().spikeStrength;

    }

    public float FShTimer=0;
    public float FShCD;
    public bool CanFS;

    public float FshDura;
    public float DuringFshTimer=0;
    public bool DuringFsh;

    void Update()
    {
        if(FShTimer<FShCD)
        {
            if(transform.GetComponent<bossHealth>().isPhase2&&!DuringFsh)
            {
                FShTimer+=Time.deltaTime;  
            }
        }
        else
        {
            CanFS = true;
        }

        if(DuringFshTimer<FshDura)
        {
            if(DuringFsh)
            {
                DuringFshTimer+=Time.deltaTime;
            }
        }
        else
        {
            transform.GetComponent<Animator>().SetFloat("shooting",0);
            DuringFsh=false;
            DuringFshTimer=0;
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
        if(lastTime > 0){
        lastTime -= Time.deltaTime;
        }
        else
        {
            GameObject bu = Instantiate(BU, transform.position, Quaternion.identity);
            bu.SetActive(true);
            bu.GetComponent<Bulletsys>().tp = player.position;
            bu.GetComponent<Bulletsys>().sp = transform.position;
            lastTime = shootTime;
        }
        */
    }

    public void SelectShoot()
    {
        bossHealth Bh  = GetComponent<bossHealth>();
        if(Bh.isPhase2)
        {
            StartCoroutine(Prepareshoot2());
        }
        else
        {
            // int k = Random.Range(0,2);
            if(Random.Range(0,3)==1)
            {
                StartCoroutine(Prepareshoot2());
            }
            else
            {
                StartCoroutine(Prepareshoot1());
            }
        }
    }

    public void shooot()
    {
        Debug.Log("shooooooo0000000000000000000000000000oot");
        //if(gameObject.GetComponent<shooterAttack>().canshooot)
        //{
            GameObject bu = Instantiate(BU, transform.position, Quaternion.identity);
            bu.SetActive(true);
            bu.GetComponent<Bulletsys>().friend = false;
            bu.GetComponent<Bulletsys>().tp = spos;
            bu.GetComponent<Bulletsys>().sp = transform.position;
            bu.GetComponent<Bulletsys>().BDmg = Streng*0.5f;
            bu.GetComponent<Bulletsys>().speed = 20;
            if(gameObject.GetComponent<bossHealth>().isPhase2)
            {
                bu.GetComponent<Bulletsys>().speed = 50;
            }
            gameObject.GetComponent<shooterAttack>().canshooot = false;

        //}
        //else
        //{
        //    return;
        //}
            
    }

    IEnumerator Prepareshoot1()
    {
        if(gameObject.GetComponent<bossHealth>().isPhase2&&DuringFsh)
        {
            Dtime1 = 7f/60f;
        }
        Debug.Log("prepare");
        ppos = player.position;
        Vector3 endpos;
        Vector3 direc = player.position - transform.position;
        Quaternion rotation = Quaternion.Euler(0, 0, lilDegree);
        Vector3 DIrec = rotation * direc;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, DIrec.normalized, 500, BMask);
        endpos = hit ? (Vector3)hit.point : transform.position + DIrec*500;
        spos = transform.position + DIrec;
        
        if(!gameObject.GetComponent<bossHealth>().isPhase2)
        {
            CreateRay(transform.position,endpos,Dtime1);
        }
        else
        {
            if(DuringFsh)
            {
                CreateRay(transform.position,endpos,Dtime1*0.6f*DelayP);
            }
            else
            {
                CreateRay(transform.position,endpos,Dtime1);
            }
        }
        yield return new WaitForSeconds(Dtime1);
        shooot();
    }

    IEnumerator Prepareshoot2()
    {
        Stillpos = transform.position;
        int count = 2;
        Debug.Log("prepare222");
        ppos = player.position;
        //lineRenderer.positionCount = 2*count+3;
            Vector3 direc = Quaternion.Euler(0, 0, -DDD)*(player.position - Stillpos).normalized;
            Vector3 Direc;
            Vector3 endpos;
            RaycastHit2D hit;
        for(int i = 0; i<=count; i++)
        {
            Direc = Quaternion.Euler(0, 0, DDD*i)*direc;
            hit = Physics2D.Raycast(Stillpos, Direc.normalized, 500, BMask);
            endpos = hit ? (Vector3)hit.point : Stillpos + Direc.normalized*500;
            //Debug.Log(Direc);
            //lineRenderer.SetPosition(2*i, transform.position);
            //lineRenderer.SetPosition(2*i+1, endpos);
            CreateRay(Stillpos,endpos,Dtime2);
        }
        //lineRenderer.SetPosition(6, transform.position);
        yield return new WaitForSeconds(Dtime2);

        for(int i = 0; i<=count; i++)
        {
            Direc = Quaternion.Euler(0, 0, DDD*i)*direc;
            spos = transform.position + Direc;
            shooot();
        }
    }
    IEnumerator Prepareshoot3()
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
            yield return new WaitForSeconds(Dtime3);
            clear();

        for(int i = 0; i<count; i++)
        {
            Direc = Quaternion.Euler(0, 0, i*theta)*(ppos - transform.position).normalized;
            spos = transform.position + Direc;
            shooot();
        }

    }
    public void clear()
    {
        lineRenderer.positionCount = 0;

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

}
