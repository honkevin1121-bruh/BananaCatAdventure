using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooseCollide : MonoBehaviour
{

    public bool isboss;
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag != "exp" && other.gameObject.tag != "heart")
        {
            Debug.Log(":PPPP");
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), other.collider);
        }
    }
}
