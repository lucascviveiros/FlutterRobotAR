using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointReached : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private ScoreManager scoreManager; 
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private SequenceManager sequenceManager; 
    [SerializeField] private GameObject particleEffect;

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
        sequenceManager = FindObjectOfType<SequenceManager>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("PointReached!_: " + other.name);   
        if (other.name == "robotSphere")
        {
            PlaySound();
            scoreManager.AddNewScore();
            ActivateParticle();
            sequenceManager.Reset();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        Debug.Log("OnTriggerExit: " + other.name);   
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
