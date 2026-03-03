using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEMove : MonoBehaviour
{
    public int moveTimes;
    public float dura;
    RectTransform rect;
    Vector3 up;
    Vector3 down;
    Vector3 left;
    Vector3 right;
    public List<int> Dlist;
    public bool yourTurn;
    // Start is called before the first frame update
    void OnEnable()
    {
        dura = 0.5f;
        yourTurn = false;
        Dlist = new List<int>();
        rect = transform.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector3(0,0,0);
        up = new Vector3 (0,69,0);
        down = new Vector3 (0,-69,0);
        right = new Vector3 (55,0,0);
        left = new Vector3 (-55,0,0);

        StartCoroutine(moveSequence(moveTimes));
    }

    IEnumerator moveSequence(int n)
    {
        yield return new WaitForSecondsRealtime(1f);
        int k;
        for(int i = 1; i<=n; i++)
        {
            k = Random.Range(0,4);
            moving(k);
            Dlist.Add(k);
            yield return new WaitForSecondsRealtime(1f);
            
            if(i==n)
            {
                yield return new WaitForSecondsRealtime(dura);
                yourTurn = true;
                Debug.Log("00000000000000000000000000");
            }
            
        }
    }
    

    public void moving(int direc)
    {
        if(direc == 0)
        {
            StartCoroutine(MoveTo(up,new Vector3(0,0,0),dura));
        }
        if(direc == 1)
        {
            StartCoroutine(MoveTo(down,new Vector3(0,0,0),dura));
        }
        if(direc == 2)
        {
            StartCoroutine(MoveTo(left,new Vector3(0,0,0),dura));
        }
        if(direc == 3)
        {
            StartCoroutine(MoveTo(right,new Vector3(0,0,0),dura));
        }
    }

    IEnumerator MoveTo(Vector3 targetPos,Vector3 startPos, float duration)
    {
        
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            // 計算進度百分比 (0 到 1)
            float percent = elapsed / duration; 
            
            // 執行插值
            rect.anchoredPosition = Vector3.Lerp(startPos, targetPos, percent);
            
            yield return null; // 等待下一幀
        }

        // 確保最後精確停在目標點
        rect.anchoredPosition = targetPos;
        if(!(targetPos.x ==0 && targetPos.y == 0))
        {
            StartCoroutine(MoveTo(startPos,targetPos,dura));
        }
    }
}
