using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] bgms;
    public AudioSource source;
    private void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.loop = false;
    }

    private void Update()
    {
        if(source.isPlaying == false)
        {
            source.clip = bgms[Random.Range(0, bgms.Length)];
            source.Play();
        }
    }

}
