using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class qteAccu0 : MonoBehaviour
{

    Transform bananal;
    Transform eye;
    RectTransform rect;
    public bool nextmove;
    Vector3 ori;
    public int num;

    public float dura;
    public float dark;
    public Image backScreen;
    public int wrongnum;
    public bool istroll;
    QTEMove qTEMove;
    public watcherAtk wh;

    public float Lastime;
    public float timer;
    bool end;
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.GetChild(4).GetComponent<Image>().fillAmount = 1f;
        end = false;
        timer = 0;
        Time.timeScale = 0f;
        nextmove = true;
        bananal = transform.GetChild(5);
        eye = transform.GetChild(2).GetChild(0);
        ori = new Vector3(0,0,0);
        num = -1;
        wrongnum =0;
        qTEMove = eye.GetComponent<QTEMove>();
        bananal.GetChild(0).GetComponent<RectTransform>().anchoredPosition =  new Vector3(0,360,0);
        bananal.GetChild(1).GetComponent<RectTransform>().anchoredPosition =  new Vector3(0,-360,0);
        bananal.GetChild(2).GetComponent<RectTransform>().anchoredPosition =  new Vector3(-360,0,0);
        bananal.GetChild(3).GetComponent<RectTransform>().anchoredPosition =  new Vector3(360,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer<Lastime&&qTEMove.yourTurn)
        {
            transform.GetChild(4).GetComponent<Image>().fillAmount = 1f*(Lastime>timer ? Lastime-timer : 0f)/Lastime;
            timer+=Time.unscaledDeltaTime;
        }
        else if(qTEMove.yourTurn&&!end)
        {
            end = true;
            wrongnum += qTEMove.Dlist.Count - (num+1);
            nextmove = false;
            transform.GetChild(4).GetComponent<Image>().fillAmount = 0f;
            StartCoroutine(Wrong());
        }

        if(qTEMove.yourTurn&& Input.GetKeyDown(KeyCode.W) && nextmove)
        {
            rect = bananal.GetChild(0).GetComponent<RectTransform>();
            nextmove = false;
            StartCoroutine(MoveTo(ori,rect.anchoredPosition,0));
        }
        if(qTEMove.yourTurn&& Input.GetKeyDown(KeyCode.S) && nextmove)
        {
            rect = bananal.GetChild(1).GetComponent<RectTransform>();
            nextmove = false;
            StartCoroutine(MoveTo(ori,rect.anchoredPosition,1));
        }
        if(qTEMove.yourTurn&& Input.GetKeyDown(KeyCode.A) && nextmove)
        {
            rect = bananal.GetChild(2).GetComponent<RectTransform>();
            nextmove = false;
            StartCoroutine(MoveTo(ori,rect.anchoredPosition,2));
        }
        if(qTEMove.yourTurn&& Input.GetKeyDown(KeyCode.D) && nextmove)
        {
            rect = bananal.GetChild(3).GetComponent<RectTransform>();
            nextmove = false;
            StartCoroutine(MoveTo(ori,rect.anchoredPosition,3));
        }
        
        
    }

    IEnumerator MoveTo(Vector3 targetPos,Vector3 startPos,int d)
    {
        if(targetPos.x ==0 && targetPos.y == 0)
        {
            qTEMove.dura = dura;
            qTEMove.moving(qTEMove.Dlist[num+1]);
        }

        float elapsed = 0f;

        while (elapsed < dura)
        {
            elapsed += Time.unscaledDeltaTime;
            // 計算進度百分比 (0 到 1)
            float percent = elapsed / dura; 
            
            // 執行插值
            rect.anchoredPosition = Vector3.Lerp(startPos, targetPos, percent);
            
            yield return null; // 等待下一幀
        }

        // 確保最後精確停在目標點
        rect.anchoredPosition = targetPos;
        if(targetPos.x ==0 && targetPos.y == 0)
        {
            num += 1;
            RightWrong(d);
            StartCoroutine(MoveTo(startPos,targetPos,0));
        }
        else
        {
            if(num < qTEMove.Dlist.Count-1)
            {
                nextmove = true;
            }
        }
    }

    public void RightWrong(int dir)
    {
        if(dir == eye.GetComponent<QTEMove>().Dlist[num])
        {
            //Debug.Log("Right:))))))))))))))))))");
            StartCoroutine(Right());
        }
        else
        {
            wrongnum += 1;
            //Debug.Log("Wrong:(((((((((((((((((((((((");
            StartCoroutine(Wrong());
        }
    }

    IEnumerator Wrong()
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
        qteEnd();

        
    }


    IEnumerator Right()
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
        qteEnd();
        
    }

    void qteEnd()
    {
        if(num == qTEMove.Dlist.Count-1||timer>=Lastime)
        {
            if(wrongnum==0)
            {
                if(!istroll)
                {
                    wh.shieldnum-=1;
                    wh.CS();
                }
                else 
                {
                    wh.freeze(3);       
                }
            }
            
            GameObject.Find("player").GetComponent<playerProperties>().qteResult(wrongnum);
            
            Time.timeScale = 1f;
            Debug.Log(wrongnum);
            gameObject.SetActive(false);
        }
    }

    
}
