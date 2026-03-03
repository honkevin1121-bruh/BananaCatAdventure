using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerProperties : MonoBehaviour
{

public int lvl;
public float exp;
public float ExpNeeded;
public float speed;
public float maxhealth;
public float Chealth;
public float atkStrength;
public Vector3 position;

public int healL;

public GameObject B1;
public GameObject Title;
public GameObject ASys;
public GameObject SPPP;
public GameObject BNNN;

public int shieldLv;
public int bomberl;

public List<int> LevelList;
public List<float> TimerList;
public List<bool> CountList;
public List<float> DuraList;
public List<charging> charList;
public List<TextMeshProUGUI> textList;
public List<int> SPOol;

public Text record;
public AudioSource audioSource;
public AudioSource audioSource1;
public AudioClip lvlupup;
public AudioClip GEtexp;


public GameObject skillList;


public TextMeshProUGUI healthtextObject;
public TextMeshProUGUI strengthtextObject;
public TextMeshProUGUI speedtextObject;
public TextMeshProUGUI MagicCirtextObject;
public TextMeshProUGUI SabertextObject;
public TextMeshProUGUI healstextObject;
public int maxhealthLvl;
public int strengthLvl;
public int speedLvl;


void Awake()
{

	// transform.GetChild(7).gameObject.SetActive(false);
	// transform.GetChild(8).gameObject.SetActive(false);
	bomberl = 0;
	shieldLv = 0;
	healL = 0;
	speed = 10;
	atkStrength = 5;
	maxhealth = 2000;
    lvl = 0;
	ExpNeeded = 10*Mathf.Pow(2,lvl-1);
	record.text = "Lvl : 0 \nExp : 0";

	maxhealthLvl = 0;
	strengthLvl = 0;
	speedLvl = 0;

	for (int i=0;i<=4;i++)
	{
		//Debug.Log("00000000000000000000000000000000");
		LevelList.Add(0);
		CountList.Add(false);
		TimerList.Add(0);
		DuraList.Add(3);
		charList.Add(skillList.transform.GetChild(0).GetChild(0).GetComponent<charging>());
		textList.Add(skillList.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>());
		transform.GetChild(7+i).gameObject.SetActive(false);
	}

	for (int i=0;i<=6;i++)
	{
		skillList.transform.GetChild(i).gameObject.SetActive(false);
	}
	transform.GetChild(11).GetChild(0).gameObject.SetActive(false);
	transform.GetChild(10).GetChild(0).gameObject.SetActive(false);
	transform.GetChild(11).GetChild(1).gameObject.SetActive(false);
	transform.GetChild(10).GetChild(1).gameObject.SetActive(false);
		

	// magiccircleLvl = 0;
	// SaberLvl = 0;
	// MCtime = 3;
	// Stime = 3;
	// mskillCT = MSkill.transform.GetChild(0).GetComponent<charging>();
	// sskillCT = SSkill.transform.GetChild(0).GetComponent<charging>();
	// mskillCT.DT = MCtime;
	// sskillCT.DT = Stime;

	//mCount = true;
	//SCount = true;

}

private void Update() {
	

	for(int i=0;i<=4;i++)
	{
		if(LevelList[i]>0&&CountList[i])
		{
			TimerList[i] += Time.deltaTime;
			charList[i].t = TimerList[i];
		}
	}

	for(int i=0;i<=4;i++)
	{
		if(TimerList[i] >= DuraList[i] && !transform.GetComponent<Playerhealth>().pDied&&Time.timeScale != 0f)
		{
			CountList[i] = false;
			if(Input.GetKey(KeyCode.Alpha1 + i))
			{
				transform.GetChild(i+7).gameObject.SetActive(true);
				TimerList[i] = 0;
				//CountList[i] = true;
			}
		}	
	}

	

	// if(MCtimer >= MCtime && !transform.GetComponent<Playerhealth>().pDied&&Time.timeScale != 0f)
	// {
	// 	mCount = false;
	// 	if(Input.GetKeyDown(KeyCode.Alpha1))
	// 	{
	// 		transform.GetChild(8).gameObject.SetActive(true);
	// 		MCtimer = 0;
	// 		mCount = true;
	// 	}
	// }

	// if(Stimer >= Stime && !transform.GetComponent<Playerhealth>().pDied&&Time.timeScale != 0f)
	// {
	// 	SCount = false;
	// 	if(Input.GetKey(KeyCode.Alpha1))
	// 	{
	// 		transform.GetChild(7).gameObject.SetActive(true);
	// 		Stimer = 0;
	// 		SCount = true;
	// 	}
	// }

}

	public void showskill(int skinum)
	{
		// if(skinum == 1)
		// {
		// 	SSkill.SetActive(true);
		// }
		// if(skinum == 2)
		// {
		// 	MSkill.SetActive(true);
		// }

		if(skinum == 3||skinum ==4)
		{
			CountList[3] = true;
			LevelList[3] += 1;
			textList[3] = skillList.transform.GetChild(skinum).GetChild(1).GetComponent<TextMeshProUGUI>();
			transform.GetChild(10).GetChild(skinum-3).gameObject.SetActive(true);
			charList[3] = skillList.transform.GetChild(skinum).GetChild(0).GetComponent<charging>();
			skillList.transform.GetChild(skinum).GetChild(0).GetComponent<charging>().DT = DuraList[3];
		}
		else if(skinum ==5||skinum ==6)
		{
			CountList[4] = true;
			LevelList[4] += 1;
			textList[4] = skillList.transform.GetChild(skinum).GetChild(1).GetComponent<TextMeshProUGUI>();
			transform.GetChild(11).GetChild(skinum-5).gameObject.SetActive(true);
			charList[4] = skillList.transform.GetChild(skinum).GetChild(0).GetComponent<charging>();
			skillList.transform.GetChild(skinum).GetChild(0).GetComponent<charging>().DT = DuraList[4];
		}
		else
		{
			CountList[skinum] = true;
			LevelList[skinum] += 1;
			charList[skinum] = skillList.transform.GetChild(skinum).GetChild(0).GetComponent<charging>();
			skillList.transform.GetChild(skinum).GetChild(0).GetComponent<charging>().DT = DuraList[skinum];
		}
			
			skillList.transform.GetChild(skinum).gameObject.SetActive(true);
	}

	public void uilvl(int uin)
	{
		if(uin == 1)
		{
			maxhealthLvl += 1;
			healthtextObject.text = "Lvl"+maxhealthLvl;
		}
		else if(uin == 2)
		{
			strengthLvl += 1;
			strengthtextObject.text = "Lvl"+strengthLvl;
		}
		else if(uin == 3)
		{
			speedLvl += 1;
			speedtextObject.text = "Lvl"+speedLvl;
		}
		else
		{
			textList[uin-4].text = "Lvl"+LevelList[uin-4];
		}

		// if(uin == 4)
		// {
		// 	SabertextObject.text = "Lvl"+LevelList[0];
		// }
		// if(uin==5)
		// {
		// 	MagicCirtextObject.text = "Lvl"+LevelList[1];
		// }
		// if(uin ==6)
		// {
		// 	healthtextObject.text = "Lvl"+LevelList[2];
		// }
		
	}

	public void qteResult(int wrongn)
	{
		if(wrongn >=4)
		{
			
		}
		else if(wrongn == 3)
		{
			
		}
		else if(wrongn ==2)
		{
			
		}
		else if(wrongn ==1)
		{
			
		}
		else
		{
			
		}
	}

	public void qteAcuResult(float acu)
	{
		if(acu >=0.9f)
		{
			
		}
		else if(acu >= 0.8f)
		{
			
		}
		else if(acu >= 0.7f)
		{
			
		}
		else
		{
			StartCoroutine(freeeezee(3));
		}
	}

	IEnumerator freeeezee(float Ft)
	{
		GetComponent<player_movement>().movespeed = 0;
		yield return new WaitForSeconds(Ft);
		GetComponent<player_movement>().movespeed = speed;
	}



public void reset()
{
	spawn SPA = ASys.GetComponent<spawn>();
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

    foreach (GameObject enemy in enemies)
    {
		if(enemy.GetComponent<enemy>() != null)
        enemy.GetComponent<enemy>().Die(0);
    }

	GameObject[] EXPs = GameObject.FindGameObjectsWithTag("exp");

    foreach (GameObject EXP in EXPs)
    {
		Destroy(EXP);
    }

	GameObject[] HPs = GameObject.FindGameObjectsWithTag("heart");

    foreach (GameObject HP in HPs)
    {
		Destroy(HP);
    }

	// if(transform.GetChild(10).GetChild(0).gameObject.activeSelf)
	// {
	// 	transform.GetChild(10).GetChild(0).GetComponent<>
	// }
	// if(transform.GetChild(10).GetChild(1).gameObject.activeSelf)
	// {
		
	// }
	//SPPP.GetComponent<spinnerScript>().ShieldActivated(0);
	SPA.BF=false;
	SPA.duringFight=false;
	BNNN.GetComponent<bomb>().bomber = 0;
	gameObject.GetComponent<p_combat>().DamageStrength = 5;
	gameObject.GetComponent<player_movement>().movespeed = 10;
	gameObject.GetComponent<player_movement>().PAni.SetFloat("status", 0f);
	SPA.RSet();
	exp = 0;
	bomberl = 0;
	shieldLv = 0;
	healL = 0;
	speed = 10 ;
	atkStrength = 5;
	maxhealth = 200;
    lvl = 0;
	ExpNeeded = 10*Mathf.Pow(2,lvl-1);
	record.text = "Lvl : 1 \nExp : 0";

	for (int i=0;i<=4;i++)
	{
		//Debug.Log("00000000000000000000000000000000");
		LevelList[i]=0;
		CountList[i]=false;
		TimerList[i]=0;
		transform.GetChild(7+i).gameObject.SetActive(false);
		// DuraList.Add(3);
		// LevelList.Add(0);
		// CountList.Add(false);
		// TimerList.Add(0);
		// charList.Add(skillList.transform.GetChild(0).GetChild(0).GetComponent<charging>());
		// textList.Add(skillList.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>());
	}
	for (int i=0;i<=6;i++)
	{
		skillList.transform.GetChild(i).gameObject.SetActive(false);
	}
	transform.GetChild(11).GetChild(0).gameObject.SetActive(false);
	transform.GetChild(10).GetChild(0).gameObject.SetActive(false);
	transform.GetChild(11).GetChild(1).gameObject.SetActive(false);
	transform.GetChild(10).GetChild(1).gameObject.SetActive(false);
}

public void BFreset()
{
	spawn SPA = ASys.GetComponent<spawn>();
	gameObject.GetComponent<player_movement>().PAni.SetFloat("status", 0f);
	for (int i=0;i<=4;i++)
	{
		if(LevelList[i]>0)
		{
			CountList[i]=true;
		}
		TimerList[i]=0;
	}
	SPA.BossFight(lvl);
	transform.GetChild(10).gameObject.SetActive(false);

}

public void SaveReset()
{
		spawn SPA = ASys.GetComponent<spawn>();
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

    foreach (GameObject enemy in enemies)
    {
		if(enemy.GetComponent<enemy>() != null)
        enemy.GetComponent<enemy>().Die(0);
    }

	GameObject[] EXPs = GameObject.FindGameObjectsWithTag("exp");

    foreach (GameObject EXP in EXPs)
    {
		Destroy(EXP);
    }

	GameObject[] HPs = GameObject.FindGameObjectsWithTag("heart");

    foreach (GameObject HP in HPs)
    {
		Destroy(HP);
    }

	//SPPP.GetComponent<spinnerScript>().ShieldActivated(0);
	SPA.BF=false;
	SPA.duringFight=false;

	gameObject.GetComponent<player_movement>().PAni.SetFloat("status", 0f);
	for (int i=0;i<=4;i++)
	{
		LevelList[i]=SaveLevelList[i];
		if(LevelList[i]>0)
		{
			CountList[i]=true;
		}
		TimerList[i]=0;
	}

	speed = Savesped;
	atkStrength = SaveatkStrength;
	maxhealth = Savemaxhealth;
    lvl = Savelvl;
	ExpNeeded = 10*Mathf.Pow(2,Savelvl-1);

	maxhealthLvl = SavemaxhealthLvl;
	strengthLvl = SavestrengthLvl;
	speedLvl = SavespeedLvl;

	transform.GetChild(10).gameObject.SetActive(false);
}

public List<bool> SaveCountList;
public List<int> SaveLevelList;

public float Savesped;
public float SaveatkStrength;
public float Savemaxhealth;
public int Savelvl;
public int SavemaxhealthLvl;
public int 	SavestrengthLvl;
public int 	SavespeedLvl;
public void SaveP()
{
	// gameObject.GetComponent<player_movement>().PAni.SetFloat("status", 0f);
	for (int i=0;i<=4;i++)
	{
		SaveCountList.Add(false);
		SaveLevelList.Add(0);
	}
	for (int i=0;i<=4;i++)
	{
		SaveLevelList[i]=LevelList[i];
		if(LevelList[i]>0)
		{
			SaveCountList[i]=true;
		}
		// TimerList[i]=0;
	}

	Savesped = speed;
	SaveatkStrength = atkStrength;
	Savemaxhealth = maxhealth;
    Savelvl = lvl;
	SavemaxhealthLvl = maxhealthLvl;
	SavestrengthLvl = strengthLvl;
	SavespeedLvl = speedLvl;
}

public void levelUP()
{

	Time.timeScale = 0f;
	GameObject.Find("Main Camera").GetComponent<cam>().enabled = false;
    Debug.Log("show ui");
	record.text = ("Lvl : "+ lvl +"\nExp : "+ exp);
	StartCoroutine(lvlupEffect());

}

IEnumerator lvlupEffect()
{

	Title.SetActive(true);
	yield return new WaitForSecondsRealtime(1f);
	B1.SetActive(true);
	SkillButton sk = B1.GetComponent<SkillButton>();
	ASys.GetComponent<spawn>().lup(lvl, maxhealth, atkStrength,speed);
	if(lvl<3)
	{
		sk.skillPool = new List<int> {0,1,2};
	}
	else if(lvl!=6||lvl!=11)
	{
		sk.skillPool = new List<int> {0,1,2,0,1,2,3,4,5};
		sk.skillPool.AddRange(SPOol);
	}
	
	
	sk.showB(lvl);
	Title.SetActive(false);
}

void OnCollisionEnter2D(Collision2D other)
{
	Debug.Log(other);
		if(other.gameObject.tag == ("exp"))
		{
			audioSource.clip = GEtexp;
            audioSource.Play();
			exp += 10;
			record.text = ("Lvl : "+ lvl +"\nExp : "+ exp);
			//play some sound?
			if(exp >= ExpNeeded)
			{
				lvl += 3;
				exp = 0;
		 		ExpNeeded = 10*Mathf.Pow(2,lvl-1);
				if(Chealth > 0)
				{
					audioSource1.clip = lvlupup;
    				audioSource1.Play();
					levelUP();
				}
			}
			else if(other.transform.GetComponent<chooseCollide>().isboss)
			{
				lvl += 1;
				exp = 0;
		 		ExpNeeded = 10*Mathf.Pow(2,lvl-1);
				if(Chealth > 0)
				{
					audioSource1.clip = lvlupup;
    				audioSource1.Play();
					levelUP();
				}
			}
			Destroy(other.gameObject);

				
				
		}
}




}

