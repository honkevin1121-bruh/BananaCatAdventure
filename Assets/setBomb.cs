using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setBomb : MonoBehaviour
{
    public float timer;
    public float dura;
    // Start is called before the first frame update
    void OnEnable()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x,mousePos.y,0);

        if(timer<dura)
        {
            timer += Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space)||timer>=dura)
        {
            transform.parent.parent.GetChild(5).GetComponent<bomb>().ManuBomb(new Vector3(mousePos.x,mousePos.y,0));
            transform.parent.parent.GetComponent<playerProperties>().CountList[3] = true;
            transform.parent.gameObject.SetActive(false);
        }
    }
}
