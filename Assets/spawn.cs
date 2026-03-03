using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;
using TMPro;


public class spawn : MonoBehaviour
{
    public GameObject E1Prefab;
    public bool CanSpawn;
    public float respawnTime = 8.0f;
    private Vector2 screenBounds;
    private GameObject player;

    public float O;
    public float EStrength;
    float atkS;
    float health;
    float sped;
    float timer;

    float ENormalHeath;
    float ENormalDmg;
    public GameObject pl;

    public Sprite norm;
    public Sprite red;
    public Sprite fat;
    public Sprite heart;

    public int numbers;
    public float Ran;

    public bool BF;
    public bool duringFight;
    public GameObject SHOOTer;
    public GameObject WATCHer;
    public GameObject FINALboss;

    void Start()
    {
        EStrength = 0;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + O, Screen.height + O, Camera.main.transform.position.z));
        player = GameObject.FindGameObjectWithTag("Player");
        timer = respawnTime;
        CanSpawn = false;

        ENormalDmg = 5;
        ENormalHeath = 15;
        respawnTime = 8.0f;
    }

    public TextMeshProUGUI BtextObject;
    public Slider BSli;
    public void RSet()
    {
        BSli.gameObject.SetActive(false);
        BtextObject.gameObject.SetActive(false);
        FINALboss.SetActive(false);
        SHOOTer.SetActive(false);
        WATCHer.SetActive(false);
        duringFight = false;
        numbers = 0;
        EStrength = 0;
        ENormalDmg = 5;
        ENormalHeath = 15;
        respawnTime = 3.5f;
    }

    void Update()
    {
        if (player != null && CanSpawn && !BF &&!duringFight)
        {
            timer -= Time.deltaTime;
            if(timer <= 0 && numbers <15)
            {
                Debug.Log("PPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP");
                SpawnEnemyNearPlayer();
                timer = respawnTime;
            }
        }

        if(BF)
        {
            if(numbers<1)
            {
                BF = false;
                BossFight(GameObject.FindGameObjectWithTag("Player").GetComponent<playerProperties>().lvl);
            }
        }
    
    }

    public void BossFight(int Lvvl)
    {
        duringFight = true;
        if(Lvvl == 5)
        {   
            Debug.Log("9999999999999999999999999999999999");
            SHOOTer.SetActive(false);
            SHOOTer.SetActive(true);
        }
        else if(Lvvl == 10)
        {
            WATCHer.SetActive(false);
            WATCHer.SetActive(true);
        }
        else if(Lvvl == 15)
        {
            FINALboss.SetActive(false);
            FINALboss.SetActive(true);
        }
        else
        {
            duringFight = false;
        }
    }
    public void lup(int n,float ihealth,float iatkS,float isped)
    {
        respawnTime = 3.5f*Mathf.Pow(0.85f, n);
        EStrength = n-1;
        health = ihealth;
        atkS = iatkS;
        sped = isped; 

        ENormalDmg = 5*( 1 + EStrength/3 );
        ENormalHeath = 15*( 1 + EStrength/3 );
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !duringFight)
        {
            CanSpawn = true;
            Debug.Log("player enter the area ,can spawn!!");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            CanSpawn = false;
            Debug.Log("player exit the area ,cannot spawn!!");
        }
    }

    private void SpawnEnemyNearPlayer()
    {
        Debug.Log("11111111111111111111111111111111111111");    

        Vector2 randomOffset = Random.insideUnitCircle.normalized * screenBounds.magnitude;
        Vector2 spawnPosition = (Vector2)player.transform.position + randomOffset*Ran;

        // 檢查敵人是否在螢幕外
        if (!IsInsideScreenBounds(spawnPosition))
        {
            if(spawnPosition.y < 59 && spawnPosition.y > -59 && spawnPosition.x > -59 && spawnPosition.x < 59)
            {
                Debug.Log(spawnPosition+"&&");
                Spawning(spawnPosition);
            }
            else
            {
                Debug.Log("3333333333333333333333333333333333333");    
                SpawnEnemyNearPlayer();
            }
        }
        else
        {
            SpawnEnemyNearPlayer();
        }

        
    }


        private bool IsInsideScreenBounds(Vector2 position)
    {
        if (position.x > screenBounds.x || position.x < -screenBounds.x ||
            position.y > screenBounds.y || position.y < -screenBounds.y )
        {
            Debug.Log("fffffffffffffffff");
            return false;
        }
            return true;
    }


    private void Spawning(Vector2 sP)
    {
        
        Debug.Log(":PPPPPPPPPPPPPPPPP");
        
        int p;
        if(EStrength > 2) //start with lvl 3
        {
            if(EStrength<4) 
            {
                p = Random.Range(1,13);
                if(health >= 560 || sped > 8)
                {  
                    if(p < 3) //12
                    {
                        spawnThing(sP, 1);
                        
                        if(pl.GetComponent<playerProperties>().Chealth < health*0.3)
                        {
                            
                            
                            int k;
                            k = Random.Range(1,7);
                            if(k<2)
                            {
                                spawnThing(sP, 3);
                            }
                            else
                            {
                                spawnThing(sP, 1);
                            }
                            
                        }
                    }
                    else
                    {
                        
                        spawnThing(sP, 0);
                    }   
                }
                else if(atkS >= 6)
                {
                    if(p < 3)
                    {
                        spawnThing(sP, 2);
                    }
                    else
                    {
                        spawnThing(sP, 0);
                    }
                }
                else
                {
                    int k;
                    k = Random.Range(1,13);
                    if(k<2)
                    {
                        spawnThing(sP, 1);
                    }
                    else if(k<3)
                    {
                        spawnThing(sP, 2);
                    }
                    else
                    {
                        spawnThing(sP,0);
                    }
                }
            }
            else if(EStrength != 4 && EStrength !=8 && EStrength !=10) //start with lvl 4
            {
                p = Random.Range(1,13);
                if(p <3) //12
                {
                    spawnThing(sP, 0);
                }
                else if(p <8) //34567
                {
                        if(pl.GetComponent<playerProperties>().Chealth < health*0.3)
                        {
                            int k;
                            k = Random.Range(1,6);
                            if(k<3)
                            {
                                spawnThing(sP, 3);
                            }
                            else
                            {
                                spawnThing(sP, 2);
                            }
                        }
                        else
                        {
                            spawnThing(sP, 2);
                        }
                }
                else if(p <13) ///89101112
                {
                        if(pl.GetComponent<playerProperties>().Chealth < health*0.1)
                        {
                            int k;
                            k = Random.Range(1,6);
                            if(k<3)
                            {
                                spawnThing(sP, 3);
                            }
                            else
                            {
                                spawnThing(sP, 1);
                            }
                        }
                        else
                        {
                            spawnThing(sP, 1);
                        }
                }
            }



            
        }
        else
        {
            spawnThing(sP, 0);
            Debug.Log(":PPPPP");
        }
    }

    private void spawnThing(Vector2 SP, int type)
    {
        numbers += 1;
        if(type == 0)   //nor
        {
                GameObject enemy = Instantiate(E1Prefab, SP, Quaternion.identity);
                Transform child = enemy.transform.GetChild(0);
                child.GetComponent<enemy>().MaxHP = ENormalHeath;
                child.GetComponent<enemy>().dmg = ENormalDmg;
                child.GetComponent<enemy>().Etype = 0;
                enemy.GetComponent<AIPath>().maxSpeed = 5;
                enemy.GetComponent<SpriteRenderer>().sprite = norm;
                enemy.SetActive(true);

        }

        if(type == 1)   //red
        {
            GameObject enemy = Instantiate(E1Prefab, SP, Quaternion.identity);
            Transform child = enemy.transform.GetChild(0);
            child.GetComponent<enemy>().MaxHP = ENormalHeath;
            child.GetComponent<enemy>().dmg = ENormalDmg*1.25f;
            child.GetComponent<enemy>().Etype = 1;
            child.GetComponent<enemy>().ES = EStrength;
            enemy.GetComponent<AIPath>().maxSpeed = 5*Mathf.Pow(1.25f, EStrength);
            enemy.GetComponent<SpriteRenderer>().sprite = red;
            enemy.transform.localScale = new Vector3(0.34f, 0.34f, 0.4f);
            enemy.GetComponent<CircleCollider2D>().radius = 1.03f;
            enemy.SetActive(true);
        }

        if(type == 2)   //fat
        {
            GameObject enemy = Instantiate(E1Prefab, SP, Quaternion.identity);
            Transform child = enemy.transform.GetChild(0);
            //Transform cc = child.transform.GetChild(0);
            child.GetComponent<enemy>().MaxHP = ENormalHeath*2.5f;
            child.GetComponent<enemy>().dmg = ENormalDmg;
            child.GetComponent<enemy>().Etype = 2;
            enemy.GetComponent<AIPath>().maxSpeed = 3;
            enemy.GetComponent<AIPath>().radius = 1.25f;
            enemy.transform.localScale = new Vector3(0.34f, 0.34f, 0.4f);
            child.GetComponent<SpriteRenderer>().sprite = fat;
            child.transform.localScale = new Vector3(2.4f, 2.4f, 0.5f);
            enemy.GetComponent<CircleCollider2D>().radius = 2.65f;
            enemy.SetActive(true);
        }
        if(type == 3)   //heart
        {
            GameObject enemy = Instantiate(E1Prefab, SP, Quaternion.identity);
            Transform child = enemy.transform.GetChild(0);
            child.GetComponent<enemy>().MaxHP = ENormalHeath*0.1f;
            child.GetComponent<enemy>().dmg = 0;
            child.GetComponent<enemy>().Etype = 3;
            enemy.transform.localScale = new Vector3(0.34f, 0.34f, 0.4f);
            enemy.GetComponent<CircleCollider2D>().radius = 1.03f;
            enemy.GetComponent<SpriteRenderer>().sprite = heart;
            enemy.GetComponent<AIPath>().maxSpeed = 5;
            enemy.SetActive(true);
        }
        if(type == 4) //exp
        {

        }
    }



}
