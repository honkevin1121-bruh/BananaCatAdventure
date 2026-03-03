using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro.Examples;


public class p_combat : MonoBehaviour
{
public Animator animatorR;
public Animator animatorL;
public GameObject rp;
public GameObject lp;
private Transform pp;
public Transform RW;
public Transform LW;

player_movement p;
SpriteRenderer spr;
public SpriteRenderer sprL;
public SpriteRenderer sprR;

public Rigidbody2D aimRb;

public float atkrange = 0.5f;
public float knockbackForce;
public LayerMask enemylayers;
public Transform AtkPoint;

public float DamageStrength;
public float AttackRate = 2f;
public float NextAtkTime = 0f;

public Sprite hurtSprite;
public Sprite normalSprite;

public float angle;
public float ProjectileForce = 10;
public bool ultimateAttack;

public GameObject BananaPrefab;

Vector2 mousePos;
projectile Pp;

public Camera Camera0;
AudioSource audioSource;
cam c;

void Start()
{
	GameObject Camera = GameObject.Find("Main Camera");
	c = Camera.GetComponent<cam>();


	RW = transform.GetChild(0);
	LW = transform.GetChild(1);

	audioSource = GetComponent<AudioSource>();
	animatorL = LW.GetComponent<Animator>();
	animatorR = RW.GetComponent<Animator>();
	sprL = LW.GetComponent<SpriteRenderer>();
	sprR = RW.GetComponent<SpriteRenderer>();
	spr = GetComponent<SpriteRenderer>();
	pp = GetComponent<Transform>();
	p = GetComponent<player_movement>();

	ultimateAttack = false;
//	Pp = GameObject.Find("bulletSystem").GetComponent<projectile>();
	aimRb = GameObject.Find("Paim").GetComponent<Rigidbody2D>();

	DamageStrength = GetComponent<playerProperties>().atkStrength;

}




void Update()
{

	if(Time.time >= NextAtkTime)
	{
		if (Input.GetMouseButtonDown(0)&&Time.timeScale==1f)
        {
        	Vector3 clickPosition = Input.mousePosition;
            
            clickPosition = Camera.main.ScreenToWorldPoint(clickPosition);

			if(clickPosition.x >= pp.position.x)
			{

			AtkPoint = rp.GetComponent<Transform>();
			animatorR.SetTrigger("atk");
			spr.flipX = true;
			}
			else
			{
			
			AtkPoint = lp.GetComponent<Transform>();
			animatorL.SetTrigger("atk");
			spr.flipX = false;
			}

			StartCoroutine(Attack());
			NextAtkTime  = Time.time + 1f / AttackRate;



		}
		else
		{
			p.isAtking = false;
		}



		//shoot aiming :P

		mousePos = Camera0.ScreenToWorldPoint(Input.mousePosition);
		
		if(Input.GetKeyDown(KeyCode.Space) && ultimateAttack)
		{
				Pp.SHOOT();		//angle, gameObject.transform, BananaPrefab, ProjectileForce
		}

		Vector2 lookDir = mousePos - aimRb.position;
		angle = Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg - 0f; 
		//90 degree is an offset,u can adjust it urself :)

		aimRb.rotation = angle;
	}



		
		
}
IEnumerator Attack()
{
		PlaySound("slash");

		StartCoroutine(atking());

		yield return new WaitForSeconds(0.15f);

		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AtkPoint.position, atkrange, enemylayers);

		if(hitEnemies.Length > 0)
		{
			//c.Shake();
		}

		foreach(Collider2D enemy in hitEnemies)
		{
			if(enemy.tag == "boss")
			{
				Transform BT = enemy.transform;	
				if(BT.gameObject.GetComponent<bossHealth>().chealth > 0)
				{
					BT.gameObject.GetComponent<bossHealth>().BTakeDamage(DamageStrength);
					c.Shake();
					PlaySound("Anime Punch  Sound Effect for editing");
					KnockbackEnemy(enemy.transform,gameObject.transform,DamageStrength);
				}
				
			}
			else if(enemy.tag == "eye")
			{
				if(enemy.transform.parent.parent.GetComponent<Animator>().GetFloat("phase2")<=0&&enemy.transform.parent.parent.GetComponent<watcherAtk>().shieldnum>0)
				{
					enemy.transform.GetComponent<lileye>().Hit();
				}
			}
			else if(enemy.tag == "enemy")
			{
				c.Shake();
				Transform GT = enemy.transform;
				Debug.Log(GT);
        		Transform CT = GT.GetChild(0);
				CT.gameObject.GetComponent<enemy>().TakeDamage(DamageStrength);
				PlaySound("Anime Punch  Sound Effect for editing");
				KnockbackEnemy(GT,gameObject.transform,DamageStrength);
			}


		}
		
}

void OnDrawGizmosSelected()
{
	if(AtkPoint == null)

	return;
	
	Gizmos.DrawWireSphere(AtkPoint.position, atkrange);
}

IEnumerator atking()
{
	p.isAtking = true;
	yield return new WaitForSeconds(0.1f);
}

	public void KnockbackEnemy(Transform enemyTransform, Transform attackerTransform, float f)
    {
		Debug.Log("ssssssssssssssssssssssssssssssssssssss"+f);

		knockbackForce = f*4.5f;
		
        // 計算擊退方向
        Vector2 knockbackDirection = (enemyTransform.position - attackerTransform.position).normalized;

        // 應用擊退力
        Rigidbody2D enemyRigidbody = enemyTransform.GetComponent<Rigidbody2D>();
        if (enemyRigidbody != null)
        {
            enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogWarning(":O ?");
        }
    }

	public void PlaySound(string clipName)
	{
		audioSource.clip = Resources.Load<AudioClip>(clipName);
		audioSource.Play();
	}

	public void Phurt()
	{
		StartCoroutine(Hurt());
	}
	IEnumerator Hurt()
    {
        spr.sprite = hurtSprite;

        yield return new WaitForSeconds(0.1f);
        spr.sprite = normalSprite;
    }
    
    }





