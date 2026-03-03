using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raydel : MonoBehaviour
{

    float timer;
    public float dura;
    // Start is called before the first frame update
    void Start()
    {
        timer = dura;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
