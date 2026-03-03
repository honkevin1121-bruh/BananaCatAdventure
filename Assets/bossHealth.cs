using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class bossHealth : MonoBehaviour
{
    Animator ani;
    string Ona;
    public float maxhealth;
    public Slider bhealthSlider;
    public TextMeshProUGUI BtextObject;
    public float chealth;
    public bool isPhase2;
    // Start is called before the first frame update
    
    public void SetBHealth()
    {
        Ona = gameObject.name;
        //maxhealth = 1000;
        ani = GetComponent<Animator>();

        if(Ona == "boss")
        {
            maxhealth = 1000;
        }
        else if(Ona == "shooter")
        {
            maxhealth = 500;
        }
        else
        {
            maxhealth = 15;
        }
        chealth = maxhealth;
        UpdateHealthUI();
    }

    public void BTakeDamage(float dmg)
    {
        if(Ona == "boss")
        {
            if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Transforming") && chealth >0)
            {
               chealth -= dmg;
            }

            if(chealth <= 0)
            {
                if(!ani.GetBool("SecondStage"))
                {
                    ani.SetBool("SecondStage", true);
                    maxhealth = 1500;
                    chealth = maxhealth;
                    // isPhase2=true;
                }
                else
                {
                   Die();
                }

            }
        }
        else
        {
            if(Ona=="watcher")
            {
                if(transform.GetComponent<watcherAtk>().shieldnum>0)
                {
                    chealth -= 1f;
                }
                else
                {
                    chealth -= dmg;
                }
            }
            else
            {
                chealth -= dmg;
            }

            if(chealth <= 0)
            {
                if(isPhase2)
                {
                    Die();
                }
                else
                {
                    chealth =0;
                    if(Ona=="watcher")
                    {
                        transform.GetComponent<watcherAtk>().shieldnum=0;
                        transform.GetComponent<watcherAtk>().CS();
                    }
                    ani.SetFloat("phase2",1.1f);
                }
            }
            else if(chealth <= maxhealth*0.5)
            {
                // ani.SetBool("angry", true);
            }
            UpdateHealthUI();
        }

    }

    void UpdateHealthUI()
    {
        if (bhealthSlider != null)
        {
           bhealthSlider.value = (float)chealth / maxhealth;
        }
    }   

    public bool died;
    public GameObject ExpPrefab;
    public void Die()
    {
        //die
        if(Ona == "shooter")
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetComponent<ThrowThing>().enabled=false;
            transform.GetComponent<shootThing>().enabled=false;
        }
        if(Ona == "watcher")
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(false);
            transform.GetComponent<watcherAtk>().enabled=false;
            transform.GetComponent<watcherAtk>().shieldnum=0;
            transform.GetComponent<watcherAtk>().CS();
        }
        GetComponent<Animator>().SetFloat("die",1);
        died =true;
       
        bhealthSlider.gameObject.SetActive(false);
        BtextObject.gameObject.SetActive(false);

    }

    public void bye()
    {
        gameObject.SetActive(false);
    }
    public void exp()
    {
        
        GameObject Exp = Instantiate(ExpPrefab, gameObject.transform.position, Quaternion.identity);
        Exp.GetComponent<chooseCollide>().isboss = true;
        Exp.SetActive(true);
    }

    public void SHTranReset()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        isPhase2=true;
        maxhealth = 15;
        chealth = 15;
        UpdateHealthUI();
        transform.GetChild(0).localScale = new Vector3 (2.68f,2.68f,0);
        transform.GetComponent<ThrowThing>().lastTime*=0.7f;
        transform.GetComponent<shootThing>().Streng*=1.5f;
        transform.GetComponent<shooterAttack>().spikeStrength*=1.2f;
        transform.GetComponent<shooterAttack>().shootTime*=0.7f;
        GetComponent<Collider2D>().enabled = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cam>().Shake();
        // GameObject.FindGameObjectWithTag("player").GetComponent<player_movement>().movespeed = OriSped;
    }


    public void WHTranReset()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        isPhase2=true;
        maxhealth = 15;
        chealth = 15;
        UpdateHealthUI();
        transform.GetComponent<watcherAtk>().whStrength*=1.2f;
        transform.GetComponent<watcherAtk>().lastTime*=0.7f;
        transform.GetComponent<watcherAtk>().Ftimer*=0.7f;
        transform.GetComponent<watcherAtk>().shieldnum=3;
        GetComponent<Collider2D>().enabled = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cam>().Shake();
        // GameObject.FindGameObjectWithTag("player").GetComponent<player_movement>().movespeed = OriSped;
    }

    public void FBTranReset()
    {
        // transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        isPhase2=true;
        maxhealth = 15;
        chealth = 15;
        UpdateHealthUI();
        transform.GetComponent<bossDash>().dashStrength*=1.2f;
        GetComponent<bossDash>().skillCD.Add(7);
        GetComponent<bossDash>().skillCD.Add(6);
        GetComponent<bossDash>().skillCD.Add(2);
        GetComponent<bossDash>().skillCD.Add(3);
        GetComponent<bossDash>().sendCD*=0.75f;
        for(int i=0;i<4&&GetComponent<bossHealth>().isPhase2;i++)
        {
            GetComponent<bossDash>().skilltimer.Add(0);
            GetComponent<bossDash>().canskill.Add(false);
        }
        GetComponent<Collider2D>().enabled = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cam>().Shake();
        // GameObject.FindGameObjectWithTag("player").GetComponent<player_movement>().movespeed = OriSped;
    }

    public void noCollide()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    float OriSped;

    public void stop()
    {
        OriSped = GameObject.FindGameObjectWithTag("player").GetComponent<player_movement>().movespeed;
        GameObject.FindGameObjectWithTag("player").GetComponent<player_movement>().movespeed = 0;
    }

    
}
