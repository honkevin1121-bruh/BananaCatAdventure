using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowThing : MonoBehaviour
{
    public float throwTime;
    public float lastTime;
    public float offset;
    public int n;
    public float TD;
    public float H;
    public float Bombrange;
    public float BSt;
    public GameObject StP;

    Transform pp;
    // Start is called before the first frame update
    void Start()
    {
        lastTime = throwTime;
        pp = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(lastTime > 0)
        {
            if(GameObject.Find("player").GetComponent<player_movement>().movespeed > 0&&transform.GetComponent<bossHealth>().chealth>0)
            {
                lastTime -= Time.deltaTime;
            }
        }
        else
        {
            Debug.Log("ooooooooooooooooooooooooooooooooo");
            
            if(transform.GetComponent<bossHealth>().isPhase2)
            {
                if(transform.GetComponent<bossHealth>().chealth>0)
                {
                    lastTime = throwTime;
                    if(Random.Range(0,2)==1)
                    {
                        throwww(pp.position,BSt);
                    }
                    else
                    {
                        multithrowww(pp.position);
                    }
                }
               
            }
            else
            {
                if(transform.GetComponent<bossHealth>().chealth>0)
                {
                    lastTime = throwTime;
                    throwww(pp.position,BSt);   
                }
            }
            
        }

        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     multithrowww(pp.position);
        // }
    }
    
    public void throwww(Vector3 tarpos,float strength)
    {
        Vector3 mid = new Vector3 ((transform.position.x+tarpos.x)*0.5f,(transform.position.y+tarpos.y)*0.5f+H,0f);
        GameObject Stone = Instantiate(StP, transform.position, Quaternion.identity);
        Stone.SetActive(true);
        Stone.GetComponent<projectileMotion>().Set(transform.position,mid,tarpos,TD,strength);
        Stone.GetComponent<projectileMotion>().Range = Bombrange;
        //Stone.GetComponent<projectileMotion>().g = -30;
        //Stone.GetComponent<projectileMotion>().p = (Vector2)tarpos;
    }

    public void multithrowww(Vector3 tarpos)
    {
        Vector3 ppp = pp.position;
        Vector3 tp;
        /*for(int i = 0; i<= n;i++)
        {
            tp = new Vector3(ppp.x+Random.Range(1f,offset)*Mathf.Pow(-1, i),ppp.y+Random.Range(1f,offset)*Mathf.Pow(-1, i),0f);
            throwww(tp);
            
        }*/
        tp = new Vector3(ppp.x+offset,ppp.y,0f);
        throwww(tp,BSt);
        tp = new Vector3(ppp.x-offset,ppp.y,0f);
        throwww(tp,BSt);
        tp = new Vector3(ppp.x,ppp.y+offset,0f);
        throwww(tp,BSt);
        tp = new Vector3(ppp.x,ppp.y-offset,0f);
        throwww(tp,BSt);
    }

        void OnDrawGizmosSelected()
    {
	    Gizmos.DrawWireSphere(transform.position, Bombrange);
    }
}
