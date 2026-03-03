using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    //public float Timer;
    public int bomber;
    public float bombRange;
    public LayerMask enemylayers;
    public GameObject BomB;
    public AudioSource audioSource;
    Vector3 bombPos;
    cam c;

    private void Start() {
        GameObject Camera = GameObject.Find("Main Camera");
	    c = Camera.GetComponent<cam>();
    }

    // Update is called once per frame
    
    void Update()
    {
        // if(isThereEnemy() && bomber > 0)
        // {
        //     Timer += Time.deltaTime;
        //     if(Timer >= 7*Mathf.Pow(0.75f,bomber))
        //     {
        //         booomb();
        //         Timer = 0;
        //     }
        // }
    }
    
/*
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isThereEnemy())
            {
                booomb();
            }
            else
            {
                Debug.Log("no enemy :))))))))))) !!");
            }
        }
    }
*/

    public void ManuBomb(Vector3 tarpos)
    {
        Vector3 tpos = new Vector3(tarpos.x,tarpos.y+40f,0);
        GameObject boo = Instantiate(BomB, tpos, Quaternion.identity);
        boo.transform.localScale = new Vector3(1.7f,1.35f,1);
        StartCoroutine(MoveToPosition(boo, tarpos, 0.75f));
    } 

    public void booomb()
    {

        Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(transform.position, bombRange, enemylayers);

        Collider2D closestOne = null;
        float Mindis = Mathf.Infinity;
        

		foreach(Collider2D enemy in detectedEnemies)
		{
            float dis = Vector2.Distance(transform.position, enemy.transform.position);

            if(dis < Mindis)
            {
                Mindis = dis;
                closestOne = enemy;
            }

		}

        if(closestOne != null)
        {
            Debug.Log(closestOne);
            bombPos = new Vector3(closestOne.gameObject.transform.position.x,closestOne.gameObject.transform.position.y+5,closestOne.gameObject.transform.position.z);
        }

        GameObject boo = Instantiate(BomB, bombPos, Quaternion.identity);
        boo.transform.localScale = new Vector3(1.7f,1.35f,1);
        StartCoroutine(MoveToPosition(boo, closestOne.gameObject.transform.position, 0.25f));

        
    }

    bool isThereEnemy()
    {
        Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(transform.position, bombRange, enemylayers);

		return detectedEnemies.Length > 0;
    }

    void OnDrawGizmosSelected()
    {
	    Gizmos.DrawWireSphere(transform.position, bombRange);
    }

    IEnumerator MoveToPosition(GameObject B, Vector3 target, float duration)
    {
        Vector3 startPosition = B.transform.position;
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            B.transform.position = Vector3.Lerp(startPosition, target, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        B.transform.position = target;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(B.transform.position, bombRange, enemylayers);

		foreach(Collider2D enemy in hitEnemies)
		{
			Transform GT = enemy.transform;
			Debug.Log(GT);
        	Transform CT = GT.GetChild(0);
			CT.gameObject.GetComponent<enemy>().TakeDamage(GetComponentInParent<playerProperties>().atkStrength*2*bomber);
            c.Shake();
            audioSource.Play();
            
		}
        yield return new WaitForSeconds(0.05f);
        Destroy(B);
    }
}
