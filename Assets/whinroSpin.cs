using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whinroSpin : MonoBehaviour
{
    public float Rsped;
    float Rangle;
    public float R;
    // Start is called before the first frame update
    void Start()
    {
        // Rsped = 200;
        // gameObject.SetActive(true);
        //if(gameObject.name == "SpikesDad"){reeeset();}
    }

    // Update is called once per frame
    void Update()
    {
        Rangle = Rsped*Time.deltaTime;
        transform.Rotate(0,0,Rangle);

        for(int i=0;i<3;i++)
        {
            transform.GetChild(i).localPosition = new Vector3(R*Mathf.Cos(i*120f*Mathf.Deg2Rad),R*Mathf.Sin(i*120f*Mathf.Deg2Rad),0);
        }
    }
}
