using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunspin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
        // A common approach for 2D is to ignore the z component:

        // 3. Calculate the direction vector from the player to the mouse
        Vector3 direction = mousePos - transform.position;

        transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
}
