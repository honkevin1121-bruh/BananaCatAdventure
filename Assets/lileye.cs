using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lileye : MonoBehaviour
{
    public GameObject qte1;
    public GameObject qte2;
    public float eyeDura;
    public float timer;
    public float dura;
    public bool isout;
    // Start is called before the first frame update
    void Start()
    {
        isout = true;
    }

    private void Update() {
        if(timer<eyeDura&&!isout&&transform.parent.parent.GetComponent<watcherAtk>().shieldnum>0)
        {
            timer+=Time.deltaTime;
        }
        else if(!isout&&transform.parent.parent.GetComponent<watcherAtk>().shieldnum>0)
        {
            StartCoroutine(comeOut());
        }
    }

    IEnumerator hide()
    {
        float k;
        float T=0;
        while(T<dura)
        {
            k = Mathf.Lerp(0f,1f,T/dura);
            transform.localPosition = new Vector3(0,3f*(1-k),0);
            T+=Time.deltaTime;
            yield return null;
        }
        transform.localPosition = new Vector3(0f,0f,0f);
        isout = false;
    }

    IEnumerator comeOut()
    {
        float k;
        float T=0;
        while(T<dura)
        {
            k = Mathf.Lerp(0f,1f,T/dura);
            transform.localPosition = new Vector3(0,k*3f,0);
            T+=Time.deltaTime;
            yield return null;
        }
        transform.localPosition = new Vector3(0f,3f,0f);
        isout = true;
    }

    public void Hit()
    {
        watcherAtk wahBoss = transform.parent.parent.GetComponent<watcherAtk>();
        if(Random.Range(0,2)==2)
        {
            if(wahBoss.transform.GetComponent<bossHealth>().isPhase2)
            {
                qte2.transform.GetChild(2).GetChild(0).GetComponent<QTEMove>().moveTimes = 5;
            }
            else
            {
                qte2.transform.GetChild(2).GetChild(0).GetComponent<QTEMove>().moveTimes = 7;
            }
            qte2.GetComponent<qteAccu0>().istroll = false;
            qte2.SetActive(true);
        }
        else
        {
            if(wahBoss.transform.GetComponent<bossHealth>().isPhase2)
            {
                qte1.transform.GetChild(3).GetComponent<QTErotate>().Rsped = 500;
            }
            else
            {
                qte1.transform.GetChild(3).GetComponent<QTErotate>().Rsped = 250;
            }
            qte1.GetComponent<QTEAccuracy>().istroll = false;
            qte1.SetActive(true);
        }
        StartCoroutine(hide());
        timer = 0;
    }
}
