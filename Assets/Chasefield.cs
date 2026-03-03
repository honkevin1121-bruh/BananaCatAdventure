using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Chasefield : MonoBehaviour
{
    public Bounds myBounds;
    Vector3 size;
    private AIPath aiPath;
    public float killtime;

    void Start()
    {
        size = new Vector3(8f, 8f, 8f);
        aiPath = GetComponentInParent<AIPath>();
    }

    private void Update() {
        myBounds = new Bounds(gameObject.transform.position, size); 
        AstarPath.active.UpdateGraphs(myBounds);
        if(!aiPath.canMove)
        {
            killtime += Time.deltaTime;
            if(killtime >= 10.5f)
            {
                Destroy(aiPath.gameObject);
                Destroy(aiPath.gameObject.transform.GetChild(0).GetComponent<enemy>().Eslider.gameObject);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("Player"))
        {
            Debug.Log("start chase!");
            aiPath.canMove = true;
            killtime = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {

        if(other.CompareTag("Player"))
        {
            Debug.Log("stop chase!");
            aiPath.canMove = false;
        }
        
    }

            void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(myBounds.center, myBounds.size);
    }
}

