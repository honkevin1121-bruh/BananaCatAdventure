using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopChase : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("Player"))
        {
            Debug.Log("start chase!");
            gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {

        if(other.CompareTag("Player"))
        {
            Debug.Log("stop chase!");
            gameObject.SetActive(false);
        }
        
    }
}
