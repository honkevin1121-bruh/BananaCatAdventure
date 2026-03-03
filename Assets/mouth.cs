using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouth : MonoBehaviour
{
    Transform Littlemouth;
    public float AtkRange;
    public float boffestX;
    public float boffestY;
    public float biteStrength;
    public float rotation; 
    float bT;
    float BitingTimes;
    public float PbT;
    public float PBitingTimes;
    public LayerMask PMask;
    Animator animator;
    cam camera;
    

    private void Start() {
        BitingTimes = PBitingTimes;
        bT = PbT;
        Littlemouth = transform.GetChild(1);
        animator = GetComponent<Animator>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cam>();
    }
    public void rotate()
    {
        rotation = transform.GetChild(0).GetComponent<eyerotate>().eyerotation;
        Littlemouth.rotation = Quaternion.Euler(0,0,rotation - 90);
    }

    public void resetrotate()
    {
        Littlemouth.rotation = Quaternion.Euler(0,0,0);
    }

    
    public void BRresetrotate()
    {
        Littlemouth.rotation = Quaternion.Euler(0,0,0);
        animator.ResetTrigger("RBite");

    }

    public void bite()
    {
        Vector3 pos = new Vector3(Littlemouth.GetChild(0).position.x + boffestX,Littlemouth.GetChild(0).position.y + boffestY,0);;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, AtkRange, PMask);

        if(colInfo != null)
        {
            camera.Shake();
            colInfo.GetComponent<Playerhealth>().TakeDamage(biteStrength);
        }
         
    }

        public void SMALLbite()
    {
        Vector3 pos = new Vector3(Littlemouth.GetChild(0).position.x + boffestX,Littlemouth.GetChild(0).position.y + boffestY,0);;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, AtkRange, PMask);

        if(colInfo != null)
        {
            camera.Shake();
            colInfo.GetComponent<Playerhealth>().TakeDamage(biteStrength);
        }

        if(BitingTimes >0)
        {
            BitingTimes -= 1;
        }
        else
        {
            animator.SetTrigger("RBite");
            BitingTimes = PBitingTimes;
            bT = PbT;
        }
         
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = new Vector3(transform.GetChild(1).GetChild(0).position.x + boffestX,transform.GetChild(1).GetChild(0).position.y + boffestY,0);
	    Gizmos.DrawWireSphere(pos, AtkRange);
    }

    private void Update() {
        if(bT > 0)
        {
            bT -= Time.deltaTime;
            if(bT <= 0)
            {
                animator.SetFloat("RBite0", 1);
            }
        }
    }
}
