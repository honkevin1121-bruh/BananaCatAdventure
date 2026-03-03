using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QTEAccuracy : MonoBehaviour
{
    public watcherAtk wh;
    public bool istroll;
    public bool getin;
    public float dura;
    public float t;
    public Image backScreen;
    public float Lasttime;
    public float timer;
    bool end;


    // Start is called before the first frame update
    void OnEnable()
    {
        count =true;
        end = false;
        transform.GetChild(1).GetComponent<Image>().fillAmount = 1f;
        timer = 0;
        transform.GetChild(5).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0,250f,0);
        Time.timeScale = 0f;
        t=0;
        getin = false;
    }
    bool count = true;
    // Update is called once per frame
    void Update()
    {
        if(timer<Lasttime)
        {
            if(count)
            {
                transform.GetChild(1).GetComponent<Image>().fillAmount = 1f*(Lasttime>timer ? Lasttime-timer : 0f)/Lasttime;
                timer+=Time.unscaledDeltaTime;
            }
        }
        else if(!end)
        {
            end = true;
            transform.GetChild(1).GetComponent<Image>().fillAmount = 0f;
            StartCoroutine(Result(0));
            transform.GetChild(5).GetComponent<QTErotate>().Spin = false;
            transform.GetChild(3).GetComponent<QTErotate>().Spin = false;
        }

        if(transform.GetChild(3).GetComponent<QTErotate>().Spin && Input.GetKey(KeyCode.Space) && timer<Lasttime)
        {
            count = false;
            getin = true;
            transform.GetChild(5).GetComponent<QTErotate>().Spin = false;
            transform.GetChild(3).GetComponent<QTErotate>().Spin = false;
        }

        if(getin)
        {
           // Debug.Log("ppppppppppppppppppppppppppppppppp");
            if(t<dura){
                transform.GetChild(5).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0,75f + 175f*(1f-t/dura),0);
                t += Time.unscaledDeltaTime;
            }
            
            if(t >= dura)
            {
                transform.GetChild(5).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0,75f,0);
                float angb = transform.GetChild(5).GetComponent<RectTransform>().localEulerAngles.z;
                if(angb == 0)
                {
                    angb = 360;
                }
                else if(angb <0)
                {
                    angb += 360;
                }
                float ange = transform.GetChild(3).GetComponent<RectTransform>().localEulerAngles.z;
                ange = ange<0 ? ange + 360 : ange;
                Debug.Log((angb,ange,angb-ange-90f,1f-((angb-ange-90f)/50f)));
                getin = false;
                t = 0;
                StartCoroutine(Result(1f-((angb-ange-90f)/50f)));
            }
        }
    }

    IEnumerator Result(float acu)
    {
        if(acu >=0.9f)
		{
			StartCoroutine(Right(0.8f));
            yield return new WaitForSecondsRealtime(0.8f);
            if(istroll)
            {
                wh.freeze(3);
            }
            else
            {
                if(wh.shieldnum >0)
                {
                    wh.shieldnum -=1;
                    wh.CS();
                }
                else
                {
                    GameObject.Find("player").GetComponent<playerProperties>().qteAcuResult(acu);  
                }
            }
		}
		else if(acu >= 0.8f)
		{
            if(wh.shieldnum >0)
                {
                    wh.shieldnum -=1;
                    wh.CS();
                }
                else
                {
                    GameObject.Find("player").GetComponent<playerProperties>().qteAcuResult(acu);  
                }

			StartCoroutine(Right(0.5f));
            yield return new WaitForSecondsRealtime(0.8f);
            if(istroll)
            {
                wh.freeze(1);
            }
            else
            {
                wh.freeze(3);
            }
		}
		else if(acu >= 0.7f)
		{
			StartCoroutine(Right(0.2f));
            yield return new WaitForSecondsRealtime(0.8f);
            // GameObject.Find("player").GetComponent<playerProperties>().qteAcuResult(acu);
		}
		else
		{
			StartCoroutine(Wrong(0.7f));
            yield return new WaitForSecondsRealtime(0.8f);
            GameObject.Find("player").GetComponent<playerProperties>().qteAcuResult(acu);  
		}
        
        //Debug.Log((ange,360-ange-90f));
        Time.timeScale = 1f;
        timer = 0;
        gameObject.SetActive(false);
    }

        IEnumerator Wrong(float dark)
    {
        float timer = 0f;

        while (timer < dura)
        {
            // 將黑色屏幕的 alpha 值逐漸增加，使其漸暗
            float alpha = Mathf.Lerp(0f, 1f, timer / dura);
            backScreen.color = new Color(dark*alpha, 0f, 0f, 0.5f);

            // 更新計時器
            timer += Time.unscaledDeltaTime;

            yield return null;
        }

        // 確保黑色屏幕完全不透明
        backScreen.color = new Color(dark, 0f, 0f, 0.5f);

        timer = 0f;

        while (timer < dura)
        {
            // 將黑色屏幕的 alpha 值逐漸增加，使其漸暗
            float alpha = Mathf.Lerp(0f, 1f, timer / dura);
            backScreen.color = new Color(dark*(1f-alpha),0f, 0f, 0.5f);

            // 更新計時器S
            timer += Time.unscaledDeltaTime;

            yield return null;
        }

        // 確保黑色屏幕完全不透明
        backScreen.color = new Color(0f,0f, 0f, 0.5f);
    }


    IEnumerator Right(float dark)
    {
        float timer = 0f;

        while (timer < dura)
        {
            // 將黑色屏幕的 alpha 值逐漸增加，使其漸暗
            float alpha = Mathf.Lerp(0f, 1f, timer / dura);
            backScreen.color = new Color(0f,dark*alpha, 0f, 0.5f);

            // 更新計時器
            timer += Time.unscaledDeltaTime;

            yield return null;
        }

        // 確保黑色屏幕完全不透明
        backScreen.color = new Color(0f,dark, 0f, 0.5f);

        timer = 0f;

        while (timer < dura)
        {
            // 將黑色屏幕的 alpha 值逐漸增加，使其漸暗
            float alpha = Mathf.Lerp(0f, 1f, timer / dura);
            backScreen.color = new Color(0f,dark*(1f-alpha), 0f, 0.5f);

            // 更新計時器
            timer += Time.unscaledDeltaTime;

            yield return null;
        }

        // 確保黑色屏幕完全不透明
        backScreen.color = new Color(0f,0f, 0f, 0.5f);
    }
}
