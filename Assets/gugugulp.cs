using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gugugulp : MonoBehaviour
{
    public enemy E;
    public Transform mom;
    public AudioSource MyAudioSource;
    public AudioSource HisAudioSource;
    public GameObject p;

    void Start()
    {
        mom = transform.parent;
        E = mom.GetChild(0).gameObject.GetComponent<enemy>();
        HisAudioSource = mom.GetChild(0).gameObject.GetComponent<AudioSource>();
        MyAudioSource.clip = Resources.Load<AudioClip>("glup");

        StartCoroutine(Bark());

    }


    IEnumerator Bark()
    {

        while (!HisAudioSource.isPlaying && !E.Died)
        {
            yield return new WaitForSeconds(Random.Range(3f, 9f));

            if(!p.GetComponent<Playerhealth>().pDied)
            {
                MyAudioSource.Play();
            }

        }
    }
}
