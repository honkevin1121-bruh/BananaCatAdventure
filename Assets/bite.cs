using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bite : MonoBehaviour
{
    public float offestY;
    public float AtkRange;
    public LayerMask PMask;
    public float biteStrength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moubite()
    {
        Vector3 pos = new Vector3(transform.position.x,transform.position.y + offestY,0);

        Collider2D colInfo = Physics2D.OverlapCircle(pos, AtkRange, PMask);

        if(colInfo != null)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cam>().Shake();
            colInfo.GetComponent<Playerhealth>().TakeDamage(biteStrength);
        }
         
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = new Vector3(transform.position.x,transform.position.y + offestY,0);
	    Gizmos.DrawWireSphere(pos, AtkRange);
    }

    public void bye()
    {
        Destroy(transform.parent.gameObject);
        // gameObject.SetActive(false);
    }
}
