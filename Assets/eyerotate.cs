using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyerotate : MonoBehaviour
{
    public float R;
    public float d;
    public float dx;
    public float dy;
    public float eyerotation;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        R = 4;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!transform.parent.GetComponent<bossDash>().isfly)
        {
            d = Vector2.Distance(player.position, transform.parent.position);
            dx = player.position.x - transform.parent.position.x;
            dy = player.position.y - transform.parent.position.y;
            transform.position = new Vector3(transform.parent.position.x + R*dx/d*2, transform.parent.position.y + R*dy/d*2, 0);
            if(dy >0)
            {
                eyerotation = 180 - Mathf.Asin(dx/d) * Mathf.Rad2Deg;
            }
            else
            {
                eyerotation = Mathf.Asin(dx/d) * Mathf.Rad2Deg;
            }
            transform.rotation = Quaternion.Euler(0,0,eyerotation);
        }
        //Debug.Log(R*dx/d);
    }
}
