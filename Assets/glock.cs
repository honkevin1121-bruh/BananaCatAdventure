using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glock : MonoBehaviour
{

    public GameObject BU;
    public int ammo;
    public float Streng;
    public Vector3 mousePos;
    // Start is called before the first frame update
    void OnEnable()
    {
        ammo = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
        Vector3 direction = mousePos - transform.position;

        transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ammo -= 1;

            GameObject bu = Instantiate(BU, transform.position, Quaternion.identity);
            bu.SetActive(true);
            bu.GetComponent<Bulletsys>().friend = true;
            bu.GetComponent<Bulletsys>().tp = new Vector3(mousePos.x,mousePos.y,0);
            bu.GetComponent<Bulletsys>().sp = transform.position;
            bu.GetComponent<Bulletsys>().BDmg = Streng;
        }

        if(ammo <=0)
        {
            transform.parent.parent.GetComponent<playerProperties>().CountList[3] = true;
            transform.parent.gameObject.SetActive(false);
        }
    }
}
