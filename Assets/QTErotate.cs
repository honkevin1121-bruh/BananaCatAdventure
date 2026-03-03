using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTErotate : MonoBehaviour
{
    public bool Spin;
    public float Rsped;
    float Rangle;
    // Start is called before the first frame update
    void OnEnable()
    {
        Spin = true;
        transform.Rotate(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Spin)
        {
            Rangle = Rsped*Time.unscaledDeltaTime;
            transform.Rotate(0,0,Rangle);
        }
    }
}
