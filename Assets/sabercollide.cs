using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sabercollide : MonoBehaviour
{
    public float saberF;
    private void Start() {
        //saberF = 1;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "enemy")
        {
            if(other.transform.GetChild(0).GetComponent<enemy>() != null)
            {
                Transform GT = other.transform;
                Transform CT = GT.GetChild(0);
                // Debug.Log("sssssssssssssssssssssssss00000000sssssssssssss");
                p_combat pcom =  transform.parent.parent.GetComponent<p_combat>();
                CT.gameObject.GetComponent<enemy>().TakeDamage(pcom.DamageStrength*saberF);
                pcom.KnockbackEnemy(GT,transform.parent,pcom.DamageStrength);
            }
            
        }
        else if(other.tag =="boss")
        {
            Debug.Log("sssssssssssssssssssssssss00000000sssssssssssss");
            p_combat pcom =  transform.parent.parent.GetComponent<p_combat>();
            other.gameObject.GetComponent<bossHealth>().BTakeDamage(pcom.DamageStrength*saberF);
            pcom.KnockbackEnemy(other.transform,transform.parent,pcom.DamageStrength);
        }
    }
}
