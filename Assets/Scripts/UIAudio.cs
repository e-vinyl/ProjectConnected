using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UIAudio : MonoBehaviour
{
    protected AudioSource source;

    private static UIAudio instance;

    public static UIAudio Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<UIAudio>();
            }

            return instance;
        }
    }

    public void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip clip)
    {
        source.PlayOneShot(clip, 1f);
    }
}
