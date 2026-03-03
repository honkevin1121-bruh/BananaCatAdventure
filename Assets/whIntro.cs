using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;


public class whIntro : MonoBehaviour
{
    GameObject CT;
    GameObject P;
    public float dis;
    public float pms;

    public float speed = 1.0f;
    public float speed2 = 1.0f;

    private float t = 2.0f;
    public float t2 = 210f;
    public float t3 = 10f;
    public float t4 = 10f;
    
    
    Vector3 tarpos;
    Vector3 startpos;

    Vector3 tarpos2;
    Vector3 startpos2;

    public TextMeshProUGUI BtextObject;
    public Slider BSli;

    // Start is called before the first frame update
    void Start()
    {
        OriR = transform.GetChild(4).GetComponent<whinroSpin>().R;
        OriS = transform.GetChild(4).GetComponent<whinroSpin>().Rsped;
        // transform.GetChild(0).gameObject.SetActive(true);
        // respike();
        CT = GameObject.Find("Main Camera");
        P = GameObject.Find("player");
       // transform.position = new Vector3 (CT.transform.position.x,CT.transform.position.y + 8.0f,0f);
        
    }
    float OriR;
    float OriS;


    // Update is called once per frame
    void Update()
    {
       
        //CT.transform.position = new Vector3 (CT.transform.position.x,CT.transform.position.y,-10f);
        if (t < 1.0f)
        {
            t += Time.deltaTime * speed; // 隨時間增加 t
            CT.transform.position = Vector3.Lerp(startpos, tarpos, t);
        }

        if (t2 < T2)
        {
            transform.GetChild(4).GetComponent<whinroSpin>().R =OriR*t2/T2;
            t2 += Time.deltaTime;
        }

        if (t3 < T3)
        {
            transform.GetChild(4).GetComponent<whinroSpin>().Rsped =OriS+OriS*t3/T3*2f;
            t3 += Time.deltaTime;
        }

        if (t4 < T4)
        {
            transform.GetChild(4).GetComponent<whinroSpin>().R =OriR-OriR*t4/T4;
            t4 += Time.deltaTime;
        }
    }
    public float T1;
    public float T2;
    public float T3;
    public float T4;

    IEnumerator ShooterIntro()
    {
        reset();
        transform.GetChild(4).GetComponent<whinroSpin>().R=0;
        for(int i=0;i<3;i++)
        {
            transform.GetChild(4).GetChild(i).localPosition = new Vector3(0,0,0);
        }
        BSli.gameObject.SetActive(false);
        BtextObject.gameObject.SetActive(false);
        transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
        transform.position = new Vector3(CT.transform.position.x - dis,CT.transform.position.y,0f);
        // Debug.Log(transform.localScale+"PPPPPPPPPPPPPPPPPPPPPPPPPPPPPP"); 
        pms = P.GetComponent<player_movement>().movespeed;
        player_movement pm = P.GetComponent<player_movement>();
        pm.movespeed = 0;
        //Time.timeScale = 0f;
        CT.GetComponent<CinemachineBrain>().enabled = false;
        startpos = new Vector3 (P.transform.position.x,P.transform.position.y,-10f);
        tarpos = new Vector3 (CT.transform.position.x - dis,CT.transform.position.y,-10f);
        t = 0f;
        yield return new WaitForSeconds(1.5f);

        transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(T1);
        t2=0f;
        yield return new WaitForSeconds(T2);
        t3=0;
        yield return new WaitForSeconds(T3-T4);
        t4=0;
        yield return new WaitForSeconds(T4);
        transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = true;
        Boooom();
        yield return new WaitForSeconds(1.5f);
        BSli.gameObject.SetActive(true);
        BtextObject.gameObject.SetActive(true);
        BtextObject.text = "The Watcher";
        GetComponent<bossHealth>().SetBHealth();
        StartCoroutine(fight());
        transform.GetChild(1).gameObject.SetActive(true);
        GetComponent<watcherAtk>().notray=true;
        //CT.transform.position = tarpos;
    }

    void reset()
    {
        bossHealth BH = GetComponent<bossHealth>();
        watcherAtk WA = GetComponent<watcherAtk>();

        BH.chealth =0;
        BH.isPhase2 = false;
        WA.whStrength=5;
        WA.notray=false;
        WA.canlockray=false;
        WA.canray=false;
        WA.canspinray=false;
        WA.shieldnum=3;
        WA.lockRayTimer=0;
        WA.spinRayTimer=0;
        WA.lastTime=WA.rayTime;
    }
    public void Boooom()
    {
        CT.GetComponent<cam>().Shake();
    }

    IEnumerator fight()
    {
        startpos = CT.transform.position;
        tarpos = new Vector3 (P.transform.position.x,P.transform.position.y,-10f);;
        t = 0f;
        yield return new WaitForSeconds(1f);
        //CT.transform.position = 
        player_movement pm = P.GetComponent<player_movement>();
        pm.movespeed = pms;
        CT.GetComponent<CinemachineBrain>().enabled = true;

    }
}
