using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RobotLimit : MonoBehaviour
{
    [SerializeField] private Collider limit;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private RobotController robotController;
    private bool doItOnce = false;

    // Start is called before the first frame update
    void Awake()
    {
        limit = GetComponent<Collider>(); 
        robotController = FindObjectOfType<RobotController>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerExit(Collider other) 
    {
        //Debug.Log("OnTriggerExit" + other.name);   
        if(other.name == "robotSphere" && !doItOnce)
        {
            doItOnce = true;
            ResetPosition();
            PlaySoundLimit();
        } 
    }

    private void LimitParticle()
    {

    }

    private void ResetPosition()
    {
        robotController.ReturnToInitial();
        doItOnce = false;
    }

    private void PlaySoundLimit()
    {
        soundManager.PlaySoundLimit();
    }
}
