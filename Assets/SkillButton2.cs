using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton2 : MonoBehaviour
{
    public GameObject Player;
    public int SS;
    public int childIndex2;
    public int childIndex;

    public GameObject otherB;

    public void showB2(int l,List<int> Spool)
    {
        if(l == 3)
        {
            watSkill2(9);
        }
        else if(l == 6)
        {
            watSkill2(7);
        }
        else if(l==11)
        {
            watSkill2(9);
        }
        else if(l==5)
            {
                 watSkill2(4);
            }
        else
        {
            int s = Random.Range(0,Spool.Count);

            watSkill2(Spool[s]);
        }
	}

    //   public void showB2(float l,int n)
    // {
    //     childIndex = n;
	// 	if(l < 3)
	// 	{
    //             int k = n;
    //             while(k == n)
    //             {
                
    //                 k = Random.Range(0,3);    

    //                 if(k != n)
    //                 {
    //                     Debug.Log("~~~~~~~~~~~~~~~" + k);
    //                     watSkill2(k);
    //                 }
    //             }
	// 	}
	// 	else
	// 	{

    //         if(l == 3)
    //         {
    //             watSkill2(5);
    //         }
    //         else if(l == 6)
    //         {
    //             watSkill2(6);
    //         }
    //         else if(l==11)
    //         {
    //             watSkill2(7);
    //         }
    //         else
    //         {
    //             int k = n;
    //             while(k == n)
    //             {
                
    //                 k = Random.Range(0,9);

    //                 if(k > 5)
    //                 {
    //                     k -= 6;
    //                     if(k != n)
    //                     {
    //                         watSkill2(k);
    //                     }
    //                 }
    //                 else
    //                 {
    //                     if(k != n)
    //                     {
    //                         watSkill2(k);
    //                     }
    //                 }
    //             }
    //         }    
	// 	}
		
		
    // }   

    
public void watSkill2(int S)
{
    // if(S > 5)
    // {
    //     childIndex2 = S-6;
    // }
    // else
    // {
    //
    // }

        childIndex2 = S;
			//set button child + S
        if (transform.childCount > childIndex2)
        {
            Transform childTransform2 = transform.GetChild(childIndex2);
            childTransform2.gameObject.SetActive(true);
            
        }
        else
        {
            Debug.LogError("Child at index " + childIndex2 + " not found");
        }
        Debug.Log(childIndex2+"~~2");
}

public void click2()
{
    if(childIndex2 == 0)
    {
        Player.GetComponent<playerProperties>().maxhealth += Player.GetComponent<playerProperties>().maxhealth/10;
        Player.GetComponent<Playerhealth>().maxHealth += Player.GetComponent<Playerhealth>().maxHealth/10;
        Player.GetComponent<playerProperties>().uilvl(1);
    }
        if(childIndex2 == 1)
    {
        Debug.Log("#"+ Player.GetComponent<playerProperties>().atkStrength);
        Player.GetComponent<playerProperties>().atkStrength += Player.GetComponent<playerProperties>().atkStrength/7.5f;
        Player.GetComponent<p_combat>().DamageStrength += Player.GetComponent<p_combat>().DamageStrength/7.5f;
        Debug.Log("#"+ Player.GetComponent<playerProperties>().atkStrength);
        Player.GetComponent<playerProperties>().uilvl(2);
    }
        if(childIndex2 == 2)
    {
        Player.GetComponent<player_movement>().movespeed += Player.GetComponent<player_movement>().movespeed/6;
        Player.GetComponent<playerProperties>().speed = Player.GetComponent<player_movement>().movespeed;
        Player.GetComponent<playerProperties>().uilvl(3);
    }

        if(childIndex2 == 3)
    {
        //heal
        Player.GetComponent<playerProperties>().showskill(2);
        Player.GetComponent<playerProperties>().uilvl(6);
    }
        if(childIndex2 == 4)
    {
        // Player.GetComponent<playerProperties>().SaberLvl += 1;
        // Player.GetComponent<playerProperties>().SCount = true;
        Player.GetComponent<playerProperties>().showskill(0);
        Player.GetComponent<playerProperties>().uilvl(4);
    }
        if(childIndex2 == 5)
    {
        // Player.GetComponent<playerProperties>().magiccircleLvl += 1;
        // Player.GetComponent<playerProperties>().mCount = true;
        Player.GetComponent<playerProperties>().showskill(1);
        Player.GetComponent<playerProperties>().uilvl(5);
    }
        if(childIndex2 == 6)
    {
        //gun
        Player.GetComponent<playerProperties>().showskill(3);
        Player.GetComponent<playerProperties>().uilvl(7);
    }
        if(childIndex2 == 7)
    {
        //bom
        Player.GetComponent<playerProperties>().showskill(4);
        Player.GetComponent<playerProperties>().uilvl(7);
        
    }
        if(childIndex2 == 8)
    {
        //shie
        Player.GetComponent<playerProperties>().showskill(5);
        Player.GetComponent<playerProperties>().uilvl(8);
    }
        if(childIndex2 == 9)
    {
        //timestop
        Player.GetComponent<playerProperties>().showskill(6);
        Player.GetComponent<playerProperties>().uilvl(8);
        
    }
    
    if(childIndex2>5)
    {
        List<int> SPOol = Player.GetComponent<playerProperties>().SPOol;
        if(!Player.GetComponent<playerProperties>().SPOol.Contains(childIndex))
        {
            SPOol.Add(childIndex);
        }
    }

    Transform childTransform0 = transform.GetChild(childIndex2);
    Transform childTransform1 = otherB.transform.GetChild(childIndex);
    Debug.Log(childTransform1 + "~~~");
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
}
