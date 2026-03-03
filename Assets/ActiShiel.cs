using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiShiel : MonoBehaviour
{
    public GameObject Spinn;
    // Start is called before the first frame update
    void OnEnable()
    {
        Spinn.GetComponent<spinnerScript>().spinAxis = GameObject.Find("player").GetComponent<Transform>();
        Spinn.GetComponent<spinnerScript>().ShieldActivated(1);

    }

    // Update is called once per frame
    void Update()
    {
        if(Spinn.transform.childCount==0)
        {
            transform.parent.parent.GetComponent<playerProperties>().CountList[4] = true;
            transform.parent.gameObject.SetActive(false);
        }
    }
}
