using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saberstrike : MonoBehaviour
{
    public float s;
    public float DT;
    public float t;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
        //s =1;
        t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(t<DT)
        {
            t += Time.deltaTime*s;
            transform.eulerAngles = new Vector3 (0,0,360f*t/DT);
        }
        else
        {
            transform.eulerAngles = new Vector3 (0,0,0);
            t = 0;
            transform.parent.GetComponent<playerProperties>().CountList[0] = true;
            gameObject.SetActive(false);
        }
    }
}
