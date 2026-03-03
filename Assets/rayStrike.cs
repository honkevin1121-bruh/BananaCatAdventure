using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayStrike : MonoBehaviour
{
       private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            other.transform.GetComponent<Playerhealth>().TakeDamage(transform.parent.GetComponent<raySplash>().Raystren);
        }
    }
}
