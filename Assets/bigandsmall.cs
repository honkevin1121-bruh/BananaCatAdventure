using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigandsmall : MonoBehaviour
{
    public RectTransform G;
    public float duration = 1f;
    public Vector3 TSize;
    public Vector3 ISize; 
    public float Etime;
    public float E1time;
    // Start is called before the first frame update
    void Start()
    {
        G = GetComponent<RectTransform>();
        G.localScale = ISize;
    }

    // Update is called once per frame
    void Update()
    {
        if(Etime < duration)
        {
            Etime += Time.deltaTime;
            float Sc = Etime / duration;

            G.localScale = Vector3.Lerp(ISize, TSize, Sc);
            if(Etime >= duration)
            {
                G.localScale = TSize;
            }

        }
        else
        {
                
                E1time += Time.deltaTime;
                float Ss = E1time / duration;
                if(E1time < duration)
                {
                    G.localScale = Vector3.Lerp(TSize, ISize,Ss);
                }
                else
                {
                        G.localScale = ISize;
                        Etime = 0;
                        E1time = 0;
                }
        }
    }
}
