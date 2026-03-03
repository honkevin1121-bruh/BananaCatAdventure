using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Playerhealth : MonoBehaviour
{
public float maxHealth = 1000f;
public float currentHealth;

public bool pDied;
public bool animationEnd;

public float lastAtkTime;

public Slider healthSlider; // 連結到敵人的血條 Slider


public Image blackScreen; // 用於漸暗的 Image 元素
public float fadeDuration; // 漸暗持續時間

public Text text; // 要變化大小的文字
public Text text2; // 要變化大小的文字
public float scaleDuration; // 放大持續時間
public float maxScale; // 最大放大倍率

Rigidbody2D rb;
public player_movement pM;

public p_combat pC;

public playerProperties ppp;
public int healLvl;

float Timer;


public AudioSource audioSource;
public AudioClip dies;
public AudioClip hueal;
public SpriteRenderer white;
spawn SPA;
public GameObject asys;

void Start()
{   
    SPA = asys.GetComponent<spawn>();
    ppp = GetComponent<playerProperties>();
    maxHealth = ppp.maxhealth;
    pDied = false;
    currentHealth = maxHealth;
    ppp.Chealth = currentHealth;
    healLvl = ppp.healL;
    fadeDuration = 2f;
    scaleDuration = 2f;
    maxScale = 1.25f;
    pC = GetComponent<p_combat>();
    pM = GetComponent<player_movement>();
    rb = GetComponent<Rigidbody2D>();
    resetDieUI();
    UpdateHealthUI();

}

    IEnumerator PHurt()
    {
        white.flipX = gameObject.GetComponent<player_movement>().spr.flipX;
        white.color = new Color(255f, 225f, 255f, 170f);

        yield return new WaitForSeconds(0.1f);
        white.color = new Color(255f, 225f, 255f, 0f);
    }

void Update() {
    if(Input.GetKey(KeyCode.R) && pDied && animationEnd)
    {
        animationEnd = false;
        gameObject.GetComponent<playerProperties>().reset();
        PRespawn();
        pDied = false;
    }
    else if(Input.GetKey(KeyCode.F) && pDied && animationEnd)
    {
        if(SPA.duringFight)
        {
            animationEnd = false;
            PRespawn();
            pDied = false;
            gameObject.GetComponent<playerProperties>().BFreset();
        }
        else if(GetComponent<playerProperties>().lvl>5)
        {
            animationEnd = false;
            PRespawn();
            pDied = false;
            gameObject.GetComponent<playerProperties>().SaveReset();
        }
    }
    else if(Input.GetKey(KeyCode.Space) && pDied && animationEnd)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // if(healLvl > 0)
    // {
    //     Timer += Time.deltaTime;
    //     if(Timer >= interval)
    //     {
    //         Heal(healLvl*3);
    //         Timer = 0;
    //     }
    // }
}

private void OnCollisionEnter2D(Collision2D other) {

    if(other.gameObject.tag == ("enemy"))
    {
        Transform GT = other.transform;
        Transform CT = GT.GetChild(0);
        enemy Ememy = CT.gameObject.GetComponent<enemy>();
        TakeDamage(Ememy.dmg);
    }

    		if(other.gameObject.tag == ("heart"))
		{
            
            audioSource.clip = hueal;
            audioSource.Play();
			Heal(Mathf.RoundToInt(maxHealth/7));
			Destroy(other.gameObject);
			//play some sound?

				
				
		}

    if(other.gameObject.name == ("boss"))
    {
        // Debug.Log("=========================");
        Transform BT = other.transform;
        bossDash BD = BT.GetComponent<bossDash>();
        if(BD.attking && Time.time - lastAtkTime >= BD.atkInterval)
        {
            Debug.Log("=========================++++++++++++++++++++");
            TakeDamage(BD.dashStrength);
            lastAtkTime = Time.time;
        }
    }
}

private void OnCollisionStay2D(Collision2D other) {
    if(other.gameObject.tag == ("enemy"))
    {
        Transform GT = other.transform;
        Transform CT = GT.GetChild(0);
        enemy Ememy = CT.gameObject.GetComponent<enemy>();
        if(Time.time - lastAtkTime >= Ememy.atkInterval)
        {
            TakeDamage(Ememy.dmg);
            lastAtkTime = Time.time;
        }
    }
    
    if(other.gameObject.name == ("boss"))
    {
        Transform BT = other.transform;
        bossDash BD = BT.GetComponent<bossDash>();
        if(BD.attking && Time.time - lastAtkTime >= BD.atkInterval)
        {
            TakeDamage(BD.dashStrength);
            lastAtkTime = Time.time;
        }
    }

}


public void TakeDamage(float damage)
{
    StartCoroutine(PHurt());
    pC.Phurt();
    pC.PlaySound("hit");
    currentHealth -= damage;
    ppp.Chealth = currentHealth;
    UpdateHealthUI();
    
        if(currentHealth <= 0)
        {
            animationEnd = false;
            pDied = true;
            StartCoroutine(D());
            PDie();
        }
}

IEnumerator D()
{
    yield return new WaitForSecondsRealtime(1.2f);
                audioSource.clip = dies;
            audioSource.Play();
}

public void Heal(float amount)
{
    currentHealth += amount;
    currentHealth = Mathf.Min(currentHealth, maxHealth); // 確保血量不超過最大值
    UpdateHealthUI();
}

void UpdateHealthUI()
{
    if (healthSlider != null)
    {
        healthSlider.value = (float)currentHealth / maxHealth; // 將目前血量映射到 Slider 的值範圍內
    }
}




void PDie()
{

    pC.sprL.sprite = null;
    pC.sprR.sprite = null;

    pM.moveDir = Vector2.zero;
    rb.velocity = Vector2.zero;
    pC.RW.gameObject.SetActive(false);
    pC.LW.gameObject.SetActive(false);

    SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        // 遍歷所有子 SpriteRenderer 組件
        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            // 禁用 SpriteRenderer 以外的其他組件
            Component[] otherComponents = renderer.GetComponents<Component>();
            foreach (Component component in otherComponents)
            {
                if (component != renderer && component is Behaviour)
                {
                    ((Behaviour)component).enabled = false;
                    
                }
            }

        }
        this.enabled = true;
        StartCoroutine(FadeToBlackCoroutine());
        StartCoroutine(TextCoroutine(text, 0));
        StartCoroutine(TextCoroutine(text2, 255));
        StartCoroutine(TextCoroutine2());
       
        

}

void PRespawn()
{

    
    pC.RW.gameObject.SetActive(true);
    pC.LW.gameObject.SetActive(true);

    SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        // 遍歷所有子 SpriteRenderer 組件
        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            
            Component[] otherComponents = renderer.GetComponents<Component>();
            foreach (Component component in otherComponents)
            {
                if (component != renderer && component is Behaviour)
                {
                    ((Behaviour)component).enabled = true;
                    
                }
            }

        }
        healLvl = 0;
        
        pM.moveDir = Vector2.zero;
        // gameObject.transform.position = new Vector3(-0.32f, -0.14f, 0f);
        currentHealth = maxHealth;
        resetDieUI();
        UpdateHealthUI();
        
}

void resetDieUI()
    {
    text.transform.localScale = new Vector3(0.6f, 0.86f, 1f);
    text.color = new Color(255f, 0f, 0f, 0f);
    text2.color = new Color(255f, 255f, 255f, 0f);
    blackScreen.color = new Color(0f, 0f, 0f, 0f);
    }



IEnumerator FadeToBlackCoroutine()
    {
        float timer = 0f;
        blackScreen.gameObject.SetActive(true);

        while (timer < fadeDuration)
        {
            // 將黑色屏幕的 alpha 值逐漸增加，使其漸暗
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            blackScreen.color = new Color(0f, 0f, 0f, alpha);

            // 更新計時器
            timer += Time.deltaTime;

            yield return null;
        }

        // 確保黑色屏幕完全不透明
        blackScreen.color = new Color(0f, 0f, 0f, 1f);
    }

IEnumerator TextCoroutine(Text t, float k)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            // 將黑色屏幕的 alpha 值逐漸增加，使其漸暗
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            t.color = new Color(255f, k, k, alpha);

            // 更新計時器
            timer += Time.deltaTime;

            yield return null;
        }

        // 確保黑色屏幕完全不透明
        t.color = new Color(255f, k, k, 1f);
    }

IEnumerator TextCoroutine2()
    {
        
        float timer = 0f;
        Vector3 initialScale = text.transform.localScale;
        Vector3 targetScale = initialScale * maxScale;

        while (timer < scaleDuration)
        {
            // 使用差值法逐漸增加文字大小
            float t = timer / scaleDuration;
            text.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            // 更新計時器
            timer += Time.deltaTime;

            yield return null;
        }

        // 確保文字大小達到目標值
        text.transform.localScale = targetScale;
        animationEnd = true;
    }


}





