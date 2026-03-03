using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikespin : MonoBehaviour
{
    public float Rsped;
    public Transform wh;
    float Rangle;
    // Start is called before the first frame update
    void Start()
    {
        Rsped = 200;
        // gameObject.SetActive(true);
        //if(gameObject.name == "SpikesDad"){reeeset();}
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Debug.Log(transform.GetChild(0).gameObject.transform.localPosition);
        Rangle = Rsped*Time.deltaTime;
        transform.Rotate(0,0,Rangle);
        /*
        if(!transform.parent.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("closeATk"))
        {
            reeeset();
        }
        if(gameObject.name == "handDad")
        {
            transform.Rotate(0,0,Rangle);
            transform.Rotate(0,Rangle,0);
            transform.Rotate(Rangle,0,0);
        }
        */
        //Debug.Log(transform.GetChild(0).gameObject.transform.localPosition);
    }

    public void reeeset()
    {
        gameObject.SetActive(true);
        transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0,0.7f,0); //up down right left
        transform.GetChild(1).gameObject.transform.localPosition = new Vector3(0,-0.7f,0);
        transform.GetChild(2).gameObject.transform.localPosition = new Vector3(0.7f,0,0);
        transform.GetChild(3).gameObject.transform.localPosition = new Vector3(-0.7f,0,0);
        //Debug.Log("ttttttttttttttttttttttttttttttttttttttttttttttt"+transform.GetChild(0).gameObject.transform.localPosition);
    }
}
