using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    public GameObject Player;
    public int SS;
    public int childIndex;
    public int childIndex2;
    public GameObject otherB;
    public List<int> skillPool;

    private void Start() {
        // skillPool.Add(0);
        // skillPool.Add(1);
        // skillPool.Add(2);
    }

    public void showB(int l)
    {
            if(l == 3)
            {
                SS = 8;
            }
            else if(l == 4)
            {
                SS = 3;
            }
            else if(l == 6)
            {
                SS = 6;
            }
            else if(l==11)
            {
                SS = 8;
            }
            else if(l==5)
            {
                SS=5;
            }
            else
            {
                int s = Random.Range(0,skillPool.Count);

                SS = skillPool[s];
                for(int i=0;i<skillPool.Count;i++)
                {
                    if(skillPool[i]==SS)
                    {
                        skillPool.RemoveAt(i);   
                    }
                }

            }
            watSkill(SS);
            SkillButton2 sk2 = otherB.GetComponent<SkillButton2>();
            otherB.SetActive(true);
            sk2.showB2(l,skillPool);
	}
    
    // public void showB(float l)
    // {
	// 	if(l < 3)
	// 	{
    //         SS = Random.Range(0,3);
    //         SkillButton2 sk2 = otherB.GetComponent<SkillButton2>();


    //         watSkill(SS);
    //         otherB.SetActive(true);
    //         sk2.showB2(l,SS);
	// 	}
	// 	else
	// 	{
    //         if(l == 3)
    //         {
    //             SS = 4;
    //         }
    //         else if(l == 4)
    //         {
    //             SS = 3;
    //         }
    //         else if(l == 6)
    //         {
    //             SS = 6;
    //         }
    //         else if(l==11)
    //         {
    //             SS = 7;
    //         }
    //         else
    //         {
    //             int s = Random.Range(0,skillPool.CountList+1);

    //             SS = skillPool[s];
    //             if(s > 5)
    //             {
    //                 SS = s-6;
    //                 watSkill(SS);
    //             }
    //             else
    //             {
    //                 SS = s;
    //                 watSkill(s);
    //             }

    //         }
    //         watSkill(SS);
    //         SkillButton2 sk2 = otherB.GetComponent<SkillButton2>();
    //         otherB.SetActive(true);
    //         sk2.showB2(l,SS);
	// 	}
		
		
    // }   

  

public void watSkill(int S)
{
    // if(S > 5)
    // {
    //     childIndex = S - 6;
    // }
    // else
    // {
    //     childIndex = S;
    // }
    
			//set button child + S
        childIndex = S;
        
        if (transform.childCount > childIndex)
        {
            Transform childTransform2 = transform.GetChild(childIndex);
            childTransform2.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Child at index " + childIndex + " not found");
        }

        Debug.Log(childIndex+"~~1");
        
}


public void click()
{

    if(childIndex == 0)
    {
        Player.GetComponent<playerProperties>().maxhealth += Player.GetComponent<playerProperties>().maxhealth/10;
        Player.GetComponent<Playerhealth>().maxHealth += Player.GetComponent<Playerhealth>().maxHealth/10;
        Player.GetComponent<playerProperties>().uilvl(1);
    }
        if(childIndex == 1)
    {
        Debug.Log("#"+ Player.GetComponent<playerProperties>().atkStrength);
        Player.GetComponent<playerProperties>().atkStrength += Player.GetComponent<playerProperties>().atkStrength/7.5f;
        Player.GetComponent<p_combat>().DamageStrength += Player.GetComponent<p_combat>().DamageStrength/7.5f;
        Debug.Log("#"+ Player.GetComponent<playerProperties>().atkStrength);
        Player.GetComponent<playerProperties>().uilvl(2);
    }
        if(childIndex == 2)
    {
        Player.GetComponent<player_movement>().movespeed += Player.GetComponent<player_movement>().movespeed/6;
        Player.GetComponent<playerProperties>().speed = Player.GetComponent<player_movement>().movespeed;
        Player.GetComponent<playerProperties>().uilvl(3);
    }

        if(childIndex == 3)
    {
        //heal
        Player.GetComponent<playerProperties>().showskill(2);
        Player.GetComponent<playerProperties>().uilvl(6);
    }
        if(childIndex == 4)
    {
        // Player.GetComponent<playerProperties>().SaberLvl += 1;
        // Player.GetComponent<playerProperties>().SCount = true;
        Player.GetComponent<playerProperties>().showskill(0);
        Player.GetComponent<playerProperties>().uilvl(4);
    }
        if(childIndex == 5)
    {
        // Player.GetComponent<playerProperties>().magiccircleLvl += 1;
        // Player.GetComponent<playerProperties>().mCount = true;
        Player.GetComponent<playerProperties>().showskill(1);
        Player.GetComponent<playerProperties>().uilvl(5);
    }
        if(childIndex == 6)
    {
        //gun
        Player.GetComponent<playerProperties>().showskill(3);
        Player.GetComponent<playerProperties>().uilvl(7);
    }
        if(childIndex == 7)
    {
        //bom
        Player.GetComponent<playerProperties>().showskill(4);
        Player.GetComponent<playerProperties>().uilvl(7);
        
    }
        if(childIndex == 8)
    {
        //shie
        Player.GetComponent<playerProperties>().showskill(5);
        Player.GetComponent<playerProperties>().uilvl(8);
    }
        if(childIndex == 9)
    {
        //timestop
        Player.GetComponent<playerProperties>().showskill(6);
        Player.GetComponent<playerProperties>().uilvl(8);
        
    }

    if(childIndex>5)
    {
        List<int> SPOol = Player.GetComponent<playerProperties>().SPOol;
        if(!Player.GetComponent<playerProperties>().SPOol.Contains(childIndex))
        {
            SPOol.Add(childIndex);
        }
    }
    
    SkillButton2 sk2 = otherB.GetComponent<SkillButton2>();
    childIndex2 = sk2.childIndex2;
    Transform childTransform0 = transform.GetChild(childIndex);
    Transform childTransform1 = otherB.transform.GetChild(childIndex2);
    Debug.Log(childIndex2+ "~~~");
    Debug.Log(otherB.transform.GetChild(childIndex2)+ "~~~");
    childTransform0.gameObject.SetActive(false);
    childTransform1.gameObject.SetActive(false);
    gameObject.SetActive(false);
    otherB.SetActive(false);

	GameObject.Find("Main Camera").GetComponent<cam>().enabled = true;
    playerProperties PPs = GameObject.FindGameObjectWithTag("Player").GetComponent<playerProperties>();
    spawn s = GameObject.Find("A_system").GetComponent<spawn>();
    if(PPs.lvl == 5 || PPs.lvl == 10 || PPs.lvl == 15)
    {
        s.BF = true;
        // s.BossFight(PPs.lvl);
    }
    else
    {
        if(PPs.lvl == 6 || PPs.lvl == 11)
        {
            PPs.SaveP();
        }
        s.BF = false;
        s.BossFight(0);
    }
    Time.timeScale = 1f;

} 

/*
0 healthmax
1 atkStrength
2 speed
3 shield
4 keepaddhealth
5 thunderstrike
*/


}
