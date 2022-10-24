using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource; 
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip clip2;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clip = Resources.Load("Sound/PointSound") as AudioClip;
        clip2 = Resources.Load("Sound/LimitSound") as AudioClip;
    }

    public void PlaySoundPoint()
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlaySoundLimit()
    {
        audioSource.PlayOneShot(clip2);
    }
}
