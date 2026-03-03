using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.UI;

public class ShootIntro : MonoBehaviour
{
    GameObject CT;
    GameObject P;
    public float dis;
    public float pms;

    public float speed = 1.0f;
    public float speed2 = 1.0f;

    private float t = 2.0f;
    private float t2 = 2.0f;
    
    Vector3 tarpos;
    Vector3 startpos;

    Vector3 tarpos2;
    Vector3 startpos2;

    public TextMeshProUGUI BtextObject;
    public Slider BSli;

    // Start is called before the first frame update
    void Start()
    {
        // transform.GetChild(0).gameObject.SetActive(true);
        // respike();
        CT = GameObject.Find("Main Camera");
        P = GameObject.Find("player");
       // transform.position = new Vector3 (CT.transform.position.x,CT.transform.position.y + 8.0f,0f);
        
    }

    // Update is called once per frame
    void Update()
    {
       
        //CT.transform.position = new Vector3 (CT.transform.position.x,CT.transform.position.y,-10f);
        if (t < 1.0f)
        {
            t += Time.deltaTime * speed; // 隨時間增加 t
            CT.transform.position = Vector3.Lerp(startpos, tarpos, t);
        }

        if (t2 < 1.0f)
        {
            t2 += Time.deltaTime * speed2; // 隨時間增加 t
            transform.position = Vector3.Lerp(startpos2, tarpos2, t2);
        }
    }

    IEnumerator ShooterIntro()
    {
        reset();
        BSli.gameObject.SetActive(false);
        BtextObject.gameObject.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.localScale = new Vector3(0,0,0);
        Debug.Log(transform.localScale+"PPPPPPPPPPPPPPPPPPPPPPPPPPPPPP"); 
        pms = P.GetComponent<player_movement>().movespeed;
        player_movement pm = P.GetComponent<player_movement>();
        pm.movespeed = 0;
        //Time.timeScale = 0f;
        CT.GetComponent<CinemachineBrain>().enabled = false;
        startpos = new Vector3 (P.transform.position.x,P.transform.position.y,-10f);
        tarpos = new Vector3 (CT.transform.position.x - dis,CT.transform.position.y,-10f);
        t = 0f;
        yield return new WaitForSeconds(1.5f);
        transform.position = new Vector3 (CT.transform.position.x,CT.transform.position.y + 8f,0f);
        startpos2 = new Vector3 (CT.transform.position.x,CT.transform.position.y + 8f,0f);
        tarpos2 = new Vector3 (CT.transform.position.x,CT.transform.position.y,0f);;
        t2=0f;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Boooom();
        yield return new WaitForSeconds(1.5f);
        BSli.gameObject.SetActive(true);
        BtextObject.gameObject.SetActive(true);
        BtextObject.text = "The Shooter";
        GetComponent<bossHealth>().SetBHealth();
        StartCoroutine(fight());
        //CT.transform.position = tarpos;
    }

    void reset()
    {
        bossHealth BH = GetComponent<bossHealth>();
        shootThing ST = GetComponent<shootThing>();
        shooterAttack SA = GetComponent<shooterAttack>();
        ThrowThing TT = GetComponent<ThrowThing>();

        BH.chealth =0;
        ST.Streng = 5;
        SA.spikeStrength = 5;
        ST.FShTimer=0;
        ST.DuringFshTimer=0;
        SA.canshooot = false;
        ST.CanFS = false;
        TT.lastTime = TT.throwTime;
        BH.isPhase2 = false;
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

    public void respike()
    {
        transform.GetChild(0).gameObject.GetComponent<spikespin>().reeeset();
        Debug.Log("ttttttttttttttttttttttttttttttt0000000000000000tttttttttttttttt");
    }
}
