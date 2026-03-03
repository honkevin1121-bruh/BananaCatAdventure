using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charging : MonoBehaviour
{

    //public float s;
    public float DT;
    public float t;
    // Start is called before the first frame update
    void Start()
    {
        t=0;
        //DT = 3;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(t/DT);
        if(t/DT <1)
        {
            transform.localScale = new Vector3 (1,1f-(t/DT),0f);
            
        }
    }
}
