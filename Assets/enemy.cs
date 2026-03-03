using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;
using TMPro.Examples;


public class enemy : MonoBehaviour
{
    public float t;

    public float MaxHP = 100f;
    public float dmg;
    public float atkInterval;
    public Vector3 targetposition;
    public Rigidbody2D rb;
    public SpriteRenderer spr;

    public Collider2D collider2D0;

    public GameObject sPrefab;
    public GameObject ExpPrefab;
    public GameObject HeartPrefab;
    public Slider Eslider;

    
	public AIDestinationSetter aiDestinationSetter;
    public AIPath aiPath;

    AudioSource audioSource;
    AudioClip Bababoi;
    AudioClip Scream;

    public AudioClip Glup;
    public SpriteRenderer white;

    float currentHP;
    public bool Died;

    public spawn s;
    public int Etype;
    

    void Start()
    {
        Died = false;
        atkInterval = 0.5f;
        currentHP = MaxHP;
        gameObject.tag = "enemy";
        sPrefab = GameObject.Find("Slider");
        //ExpPrefab = Resources.Load<GameObject>("ExpPoint");

        aiDestinationSetter = GetComponentInParent<AIDestinationSetter>();
        aiPath = GetComponentInParent<AIPath>();
        rb = GetComponentInParent<Rigidbody2D>();
        spr = GetComponentInParent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        collider2D0 = rb.gameObject.GetComponent<Collider2D>();
        white = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        white.color = new Color(255f, 225f, 255f, 0f);
        

        targetposition = gameObject.transform.position;
        aiDestinationSetter.target = GameObject.FindGameObjectWithTag("Player").transform;

        targetposition.y += 1.5f;
        GameObject uiObject = Instantiate(sPrefab, Camera.main.WorldToScreenPoint(targetposition), Quaternion.identity);
        Eslider = uiObject.GetComponent<Slider>();
        uiObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
        uiObject.transform.SetAsFirstSibling();

        Bababoi = Resources.Load<AudioClip>("Bababoi");
        Scream = Resources.Load<AudioClip>("scream");
        Glup = Resources.Load<AudioClip>("glup");

        UpdateHealthUI();
    }

    private void Update() {

        if(Died && !audioSource.isPlaying)
        {
                Destroy(transform.parent.gameObject);
        }

        if(stop)
        {
            StopTimer = 0;
            stop = false;
            transform.parent.GetComponent<AIPath>().maxSpeed= 0;
        }

        if(StopTimer<StopDura)
        {
            StopTimer += Time.deltaTime; 
        }
        else if(transform.parent.GetComponent<AIPath>().maxSpeed<= 0)
        {
            if(Etype == 0)   //nor
            {
                transform.parent.GetComponent<AIPath>().maxSpeed = 5;
            }
            if(Etype == 1)   //red
            {
                transform.parent.GetComponent<AIPath>().maxSpeed = 5*Mathf.Pow(1.25f, ES);
            }
            if(Etype == 2)   //fat
            {
                transform.parent.GetComponent<AIPath>().maxSpeed = 3;
            }
            if(Etype == 3)   //heart
            {
                transform.parent.GetComponent<AIPath>().maxSpeed= 5;
            }
            
        }

        
    }
    public float ES;
    public float StopDura;
    public float StopTimer=10;
    public bool stop;
    // public bool CountStop;
    private void LateUpdate() {
        
        targetposition = gameObject.transform.position;
        targetposition.y += 1f;
        if(Eslider != null)
        Eslider.transform.position = Camera.main.WorldToScreenPoint(targetposition);

    }

    public void PlaySound(string s)
    {
        if(s == "B")
        {
            audioSource.clip = Bababoi;
            audioSource.Play();
        }
        else if(s == "S")
        {
            audioSource.clip = Scream;
            audioSource.Play();
        }
        else if(s == "G")
        {
            audioSource.clip = Glup;
            audioSource.Play();
        }
    }
        

    public void TakeDamage(float damage)
    {
        StartCoroutine(Hurt());

        currentHP -= damage;
        UpdateHealthUI();
        StartCoroutine(DisableScriptCoroutine());

        if(currentHP <= 0)
        {
            Die(1);
            PlaySound("S");
            Debug.Log(currentHP + ":S");
        }
        else
        {
            PlaySound("B");
            Debug.Log(currentHP + ":B");
        }

    }

    void UpdateHealthUI()
    {
        Eslider.value = (float)currentHP / MaxHP; // 將目前血量映射到 Slider 的值範圍內
    }

    public void Die(int k)
    {
        s.numbers -= 1;
        collider2D0.enabled = false;
        aiPath.enabled = false;
        rb.isKinematic = true;
        spr.enabled = false;
        transform.parent.GetComponent<SpriteRenderer>().enabled = false;
        white.enabled = false;
        Destroy(Eslider.gameObject);
        Died = true;
        playerProperties PPs = GameObject.FindGameObjectWithTag("Player").GetComponent<playerProperties>();
        /*if(s.numbers < 1 && (PPs.lvl == 5 || PPs.lvl == 8 || PPs.lvl == 10))
        {
            s.BF = true;
            s.BossFight(PPs.lvl);
        }*/
        if(k == 1)
        {
        Playerhealth PP = GameObject.FindGameObjectWithTag("Player").GetComponent<Playerhealth>();
        int p = Random.Range(1,11);
        Vector3 pp;
        if(Etype == 3)
        {
            int n = Random.Range(3,8);
            
            for(int i=0; i<n; i++)
            {
                pp = new Vector3(transform.position.x + Random.Range(-0.5f,0.5f),transform.position.y + Random.Range(-0.5f,0.5f),transform.position.z);
                
                GameObject H = Instantiate(HeartPrefab, pp, Quaternion.identity);
                H.SetActive(true);
            }
        }
        else if(PP.currentHealth > PP.maxHealth*0.5f)
        {
            // Debug.Log("not big deeeeeeeeeeeeeeeeeeeeeeeeeeeal");
            // if(p < 2)
            // {
            //     GameObject H = Instantiate(HeartPrefab, gameObject.transform.position, Quaternion.identity);
            //     H.SetActive(true);
            // }
            // else
            // {
            //     GameObject Exp = Instantiate(ExpPrefab, gameObject.transform.position, Quaternion.identity);
            //     Exp.SetActive(true);
            // }

            GameObject Exp = Instantiate(ExpPrefab, gameObject.transform.position, Quaternion.identity);
            Exp.SetActive(true);
        }
        else
        {

            if(PP.currentHealth < PP.maxHealth*0.5f)
            {

            if(PP.currentHealth < PP.maxHealth*0.3f)
            {
                Debug.Log("AbooooooooooooooooooooutToDiiiiiiiiiiiiiiiiiiiiiie" + p);

                if(p < 9)
                {
                GameObject H = Instantiate(HeartPrefab, gameObject.transform.position, Quaternion.identity);
                H.SetActive(true);
                }
                else
                {
                GameObject Exp = Instantiate(ExpPrefab, gameObject.transform.position, Quaternion.identity);
                Exp.SetActive(true);
                }
            }
            else
            {
                Debug.Log("haaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaalf");
                if(p < 7)
                {
                GameObject H = Instantiate(HeartPrefab, gameObject.transform.position, Quaternion.identity);
                H.SetActive(true);
                }
                else
                {
                GameObject Exp = Instantiate(ExpPrefab, gameObject.transform.position, Quaternion.identity);
                Exp.SetActive(true);
                }
            }
            }
            else
            {
                if(p < 5)
                {
                GameObject H = Instantiate(HeartPrefab, gameObject.transform.position, Quaternion.identity);
                H.SetActive(true);
                }
                else
                {
                GameObject Exp = Instantiate(ExpPrefab, gameObject.transform.position, Quaternion.identity);
                Exp.SetActive(true);
                }  
            }     
        }
        } 
    }



    
IEnumerator DisableScriptCoroutine()
{

        // 禁用腳本
    aiPath.canMove = false;

    yield return new WaitForSeconds(t); // 再等待一秒

        // 啟用腳本
    aiPath.canMove = true;
    rb.velocity = Vector2.zero;
}

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("Player"))
        {
            if(Etype != 2)
            {
                //Debug.Log("start avoiding others!");
                aiPath.pickNextWaypointDist = 0.5f;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other) {

        if(other.CompareTag("Player"))
        {
            Debug.Log("stop avoiding others!");
            aiPath.pickNextWaypointDist = 3f;
        }
        
    }

    IEnumerator Hurt()
    {
        white.color = new Color(255f, 225f, 255f, 170f);

        yield return new WaitForSeconds(0.1f);
        white.color = new Color(255f, 225f, 255f, 0f);
    }
    

    //IEnumerator FadeOut()
    //{
        //float timer = 0f;

        //while (timer < fadeDuration)
        //{
            // 將黑色屏幕的 alpha 值逐漸增加，使其漸暗
            //float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            //spr.color = new Color(0f, 0f, 0f, alpha);

            // 更新計時器
           // timer += Time.deltaTime;

            //yield return null;
        //}

        // 確保黑色屏幕完全不透明
        //text.color = new Color(1f, 0f, 0f, 0f);
   // }


}
