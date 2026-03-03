using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raySplash : MonoBehaviour
{
    public watcherAtk wahBoss;
    public Vector3 direction;
    public float dura;
    public float Sdura;
    public float wide;
    public float length;
    public float Rwide;
    public float Rlength;
    public LayerMask play;
    public float Raystren;
    public Vector2 pos;
    float ang;
    float sign;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        ang = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //StartCoroutine(strike());
        
    }

    public void Startstrike()
    {
        StartCoroutine(strike());
    }

    public void StartSpinstrike(int ty)
    {
        StartCoroutine(strikeSpin(ty));
    }

    public bool islocked;

    public GameObject qte1;
    public GameObject qte2;
    IEnumerator strike()
    {
       
        float timer = 0f;

        if(islocked)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(33f/255f,161f/255f,0f,1f);
        }

        while (timer < dura)
        {
            // 將黑色屏幕的 alpha 值逐漸增加，使其漸暗
            float alpha = Mathf.Lerp(0f, 1f, timer / dura);
            transform.localScale = new Vector3(Rlength,Rwide*alpha, 0f);

            // 更新計時器
            timer += Time.deltaTime;

            yield return null;
        }

        // 確保黑色屏幕完全不透明
        transform.localScale = new Vector3(Rlength,Rwide, 0f);

        Vector2 center = (Vector2)transform.position + (Vector2)transform.right*(wide/2f);
        Vector2 size = new Vector2 (wide,length);
        
        float ang = transform.eulerAngles.z;

        Collider2D[] hits = Physics2D.OverlapBoxAll(center,size,ang,play);

        foreach(var hit in hits)
        {
            if(islocked)
            {
                if(Random.Range(0,2)==2)
                {
                    qte2.GetComponent<qteAccu0>().istroll = true;
                    if(wahBoss.transform.GetComponent<bossHealth>().isPhase2)
                    {
                        qte2.transform.GetChild(2).GetChild(0).GetComponent<QTEMove>().moveTimes = 6;
                    }
                    else
                    {
                        qte2.transform.GetChild(2).GetChild(0).GetComponent<QTEMove>().moveTimes = 4;
                    }
                    qte2.SetActive(true);
                }
                else
                {
                    qte1.SetActive(true);
                    qte1.GetComponent<QTEAccuracy>().istroll = true;
                }
                
            }
            else
            {
                hit.GetComponent<Playerhealth>().TakeDamage(Raystren);
            }
        }

        timer = 0f;

        while (timer < dura)
        {
            // 將黑色屏幕的 alpha 值逐漸增加，使其漸暗
            float alpha = Mathf.Lerp(0f, 1f, timer / dura);
            transform.localScale = new Vector3(Rlength,Rwide*(1-alpha), 0f);

            // 更新計時器S
            timer += Time.deltaTime;

            yield return null;
        }

        // 確保黑色屏幕完全不透明
        transform.localScale = new Vector3(0f, 0f, 0f);
        wahBoss.lastTime = wahBoss.rayTime;
        wahBoss.notray = true;
        Destroy(gameObject);
    }


    IEnumerator strikeSpin(int type)
    {
        float timer = 0f;

        while (timer < dura)
        {
            // 將黑色屏幕的 alpha 值逐漸增加，使其漸暗
            float alpha = Mathf.Lerp(0f, 1f, timer / dura);
            transform.localScale = new Vector3(Rlength,Rwide*alpha, 0f);

            // 更新計時器
            timer += Time.deltaTime;

            yield return null;
        }

        // 確保黑色屏幕完全不透明
        transform.localScale = new Vector3(Rlength,Rwide, 0f);

        

        transform.GetChild(0).GetComponent<Collider2D>().enabled = true;
        if(type ==1)
        {
            Vector2 ppos = (Vector2)GameObject.Find("player").GetComponent<Transform>().position;
            sign = -Mathf.Sign((ppos.x + direction.x/direction.y*pos.y - pos.x - direction.x/direction.y*ppos.y)*(pos.x-ppos.x));
            StartCoroutine(Spinning(360*sign,Sdura));
            yield return new WaitForSeconds(Sdura);
        }
        else
        {
            // Vector2 ppos = (Vector2)GameObject.Find("player").GetComponent<Transform>().position;
            // sign = Mathf.Sign(ppos.x + direction.x/direction.y*pos.y - pos.x - direction.x/direction.y*ppos.y);
            StartCoroutine(Spinning(120,Sdura/3f));
            yield return new WaitForSeconds(Sdura/3f);
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
            yield return new WaitForSeconds(Sdura/8f);
            StartCoroutine(Spinning(-120,Sdura/3f));
            yield return new WaitForSeconds(Sdura/3f);
        }
        // transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        timer = 0f;

        while (timer < dura)
        {
            // 將黑色屏幕的 alpha 值逐漸增加，使其漸暗
            float alpha = Mathf.Lerp(0f, 1f, timer / dura);
            transform.localScale = new Vector3(Rlength,Rwide*(1-alpha), 0f);

            // 更新計時器S
            timer += Time.deltaTime;

            yield return null;
        }

        // 確保黑色屏幕完全不透明
        transform.localScale = new Vector3(0f, 0f, 0f);
        wahBoss.lastTime = wahBoss.rayTime;
        wahBoss.spinRayTimer = 0;
        wahBoss.notray = true;
        Destroy(gameObject);
    }

    public void KYS()
    {
        wahBoss.lastTime = wahBoss.rayTime;
        wahBoss.spinRayTimer = 0;
        wahBoss.notray = true;
        Destroy(gameObject);
    }

    private void Update() {
        if(wahBoss.transform.GetComponent<bossHealth>().chealth<=0)
        {
            KYS();
        }
    }
    IEnumerator Spinning(float fullAng,float Dura)
    {
        float timer = 0f;
        while (timer < Dura)
        {
            // 將黑色屏幕的 alpha 值逐漸增加，使其漸暗
            float alpha = Mathf.Lerp(0f, 1f, timer / Dura);
            transform.rotation = Quaternion.Euler(0,0,ang + alpha*fullAng);

            // 更新計時器
            timer += Time.deltaTime;

            yield return null;
        }
        ang += fullAng;
        transform.rotation = Quaternion.Euler(0,0,ang);


    }
}
