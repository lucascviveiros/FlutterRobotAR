using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointReached : MonoBehaviour
{
    private Collider collider;
    private ScoreManager scoreManager; 
    private SoundManager soundManager;
    private GameObject particleEffect;

    private void Awake() 
    {
        var particleSystem = Resources.Load("ScifiTris 1");
        particleEffect = particleSystem as GameObject;
    }

    private void Start() 
    {
        collider = GetComponent<Collider>();
        scoreManager = FindObjectOfType<ScoreManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.name == "robotSphere")
        {
            PlaySound();
            scoreManager.AddNewScore();
            ActivateParticle();
        }
    }

    private void ActivateParticle()
    {
        var particle = Instantiate(particleEffect, this.transform.position, Quaternion.identity) as GameObject;
        particle.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        Destroy(particle, 3f);
    }

    private void PlaySound()
    {
        soundManager.PlaySoundPoint();
    }
}
