using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class test : MonoBehaviour
{
    public Bounds myBounds;
    Vector3 size;
    public float[] array = new float[] { 1f, 2f, 3f };
    public float F = 1f;

    void Update()
    {
        size = new Vector3(F, F, F);
        Vector3[] center = new Vector3[array.Length]; 
        for (int i = 0; i < array.Length; i++)
        {
            center[i] = new Vector3(array[i], array[i], array[i]); 
        }
        myBounds = new Bounds(center[0], size); 
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(":P");
            AstarPath.active.UpdateGraphs(myBounds);
        }

        
    }

        void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(myBounds.center, myBounds.size);
    }
}

