using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healEffect : MonoBehaviour
{

    public float dura;
    public Vector3 endscale;
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.5f);
        transform.localScale = new Vector3(0.7f,0.7f,0);
        StartCoroutine(Effect());
    }

    IEnumerator Effect()
    {
        Playerhealth playerhealth = GameObject.Find("player").GetComponent<Playerhealth>();
        playerhealth.Heal(playerhealth.maxHealth*0.2f);
        float t =0;
        while(t<dura)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(new Vector3(0.7f,0.7f,0f),endscale,t/dura);
            transform.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.5f*(1f-t/dura));
            yield return null;
        }

        transform.localScale = endscale;
        transform.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
        transform.parent.GetComponent<playerProperties>().CountList[2] = true;
        gameObject.SetActive(false);

    }
}
