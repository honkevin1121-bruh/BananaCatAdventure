using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    public Transform ptf;
    Transform T;
    public float shakeAmount = 0.1f;
    // 震動的持續時間
    public float shakeDuration = 0.2f;

    private float shakeTimer = 0f;

    
    void Start()
    {
        ptf = GameObject.Find("player").GetComponent<Transform>();
        T = GetComponent<Transform>();
    }

    void LateUpdate()
    {

        // 如果震動計時器大於0，進行震動
        if (shakeTimer > 0)
        {
            // 生成一個隨機的震動向量，僅在X和Y軸上
            float shakeOffset = Random.Range(-1f, 1f) * shakeAmount;
            // 將震動應用到攝影機位置
            T.position = new Vector3(T.position.x + shakeOffset, T.position.y + shakeOffset, T.position.z);

            // 減少計時器
            shakeTimer -= Time.deltaTime;
        }
    }

    // 進行震動的方法
    public void Shake()
    {
        // 設置震動計時器
        shakeTimer = shakeDuration;
    }






    
}
