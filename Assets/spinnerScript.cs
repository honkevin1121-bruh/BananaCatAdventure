using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinnerScript : MonoBehaviour
{
    public float speed = 80;
    public float r = 5;
    public float times = 0.5f;
    public float offset;
    int n;
    public GameObject ShieldPrefab;
    public int ActivateShieldLvL;
    // public int TT;
    // public float timer;

    public Transform spinAxis;
    public float dura;
    //public GameObject t;
/*

*/
    public void Start()
    {
        // timer = TT;
    }

    private void Update() {
        // if(gameObject.transform.childCount == 0 && ActivateShieldLvL > 0 && timer > 0)
        // {
        //     timer -= Time.deltaTime;    
        // }
        

        // if(timer<=0)
        // {
        //     t.SetActive(true);
        //     if(Input.GetKeyDown(KeyCode.Space))
        //     {
        //         t.SetActive(false);
        //         timer = TT;
        //         ShieldActivated(ActivateShieldLvL);
        //     }
        // }

        /*
            if(Input.GetKeyDown(KeyCode.K))
            {
                ActivateShieldLvL += 1;
            }

        */
    }
    
    public void ShieldActivated(int shieldLvl)
    {
        ActivateShieldLvL = shieldLvl;
        if(shieldLvl != 1)
        {
                for (int k = 0; k<gameObject.transform.childCount; k++)
                {
                    Transform childTransform = transform.GetChild(k);
                    Destroy(childTransform.gameObject);
                }
        }
        

        // if(shieldLvl < 5 && shieldLvl > 0)
        // {
        //     n =  shieldLvl;
        // }
        // else if(shieldLvl > 0)
        // {
        //     n = 5;
        // }
        // else
        // {
        //     r = 5;
        //     times = 0.5f;
        //     return;
        // }

        n = 3;
        float kk = 360f/n;
        for (int i = 0; i<n; i++)
        {
            GameObject baby = Instantiate(ShieldPrefab, transform.position, Quaternion.identity);
            baby.GetComponent<spin>().offset = kk*i; 
            baby.GetComponent<spin>().r = 3;
            times *= shieldLvl;
            baby.transform.SetParent(gameObject.transform);
            baby.GetComponent<spin>().pT = spinAxis;
            baby.SetActive(true);
            baby.transform.localScale = new Vector3(1+shieldLvl*0.01f,1+shieldLvl*0.01f,1);
        }
        

    }

    public void BossShieldActivated(int shieldLvl)
    {
        ActivateShieldLvL = shieldLvl;

        for (int k = 0; k<gameObject.transform.childCount; k++)
        {
            Transform childTransform = transform.GetChild(k);
            Destroy(childTransform.gameObject);
        }

        n=shieldLvl;

        for (int i = 0; i<n; i++)
        {
            GameObject baby = Instantiate(ShieldPrefab, transform.position, Quaternion.identity);
            baby.GetComponent<spin>().offset = 360/shieldLvl*i; 
            //r += shieldLvl;
            times *= shieldLvl;
            baby.transform.SetParent(gameObject.transform);
            baby.GetComponent<spin>().pT = spinAxis;
            baby.GetComponent<spin>().r = 1f;
            baby.SetActive(true);
            baby.transform.localScale = new Vector3(size,size,1);
        }
        BossSafe();

    }
    public float size;
    public void BossCloseSh()
    {
        for (int i = 0; i<ActivateShieldLvL; i++)
        {
            StartCoroutine(GetClose(i));
            StartCoroutine(Grow(i));
        }
    }

    public void BossSafe()
    {
        for (int i = 0; i<ActivateShieldLvL; i++)
        {
            StartCoroutine(Leave(i));
            StartCoroutine(Shrink(i));
        }
    }

    IEnumerator GetClose(int k)
    {
        float t = 0;
        while(t<dura)
        {
            float p = Mathf.Lerp(0f,1f,t/dura);
            transform.GetChild(k).GetComponent<spin>().r = 1f + (1-p)*(r-1);
            t += Time.deltaTime;

            yield return null;
        }
        transform.GetChild(k).GetComponent<spin>().r = 1f;
        t=0;
    }

    IEnumerator Leave(int k)
    {
        float t = 0;
        while(t<dura)
        {
            float p = Mathf.Lerp(0f,1f,t/dura);
            transform.GetChild(k).GetComponent<spin>().r = 1f + p*(r-1);
            t += Time.deltaTime;

            yield return null;
        }
        transform.GetChild(k).GetComponent<spin>().r = r;
        t=0;
    }

    IEnumerator Grow(int k)
    {
        transform.GetChild(k).GetComponent<spin>().speed = speed*2f;
        float t = 0;
        while(t<dura)
        {
            float p = Mathf.Lerp(0f,1f,t/dura);
            transform.GetChild(k).transform.localScale = new Vector3(size+size*0.75f*p,size+size*0.75f*p,1);
            t += Time.deltaTime;

            yield return null;
        }
        transform.GetChild(k).transform.localScale = new Vector3(size+size*0.75f,size+size*0.75f,1);
        t=0;
    }

    IEnumerator Shrink(int k)
    {
        float t = 0;
        while(t<dura)
        {
            float p = Mathf.Lerp(0f,1f,t/dura);
            transform.GetChild(k).transform.localScale = new Vector3(size+size*0.75f*(1-p),size+size*0.75f*(1-p),1);
            t += Time.deltaTime;

            yield return null;
        }
        transform.GetChild(k).transform.localScale = new Vector3(size,size,1);
        t=0;
        transform.GetChild(k).GetComponent<spin>().speed = speed;
    }

    public void killkids()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    
}
