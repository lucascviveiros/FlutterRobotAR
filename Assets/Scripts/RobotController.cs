using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Linq;

public class RobotController : MonoBehaviour
{
	[SerializeField] private SequenceManager sequenceManager; 
    private Vector3 rot = Vector3.zero;
	private float rotSpeed = 0f;
	private Animator anim;
    private float delaySpeed = 0.22f; //0.33f
	private bool onceAnim;
	private int totalActions = 0;
	private List<IEnumerator> actions = new List<IEnumerator>();
	private Vector3 initialPosition;
	private Quaternion initialRotation;

	//Events listened by UIManager
	public delegate void AnimationSequenceFinished();
	public static event AnimationSequenceFinished OnAnimationSequenceFinished;

	void Awake()
	{		
		anim = gameObject.GetComponent<Animator>();	
		initialPosition = transform.position;
		initialRotation = transform.rotation;
		sequenceManager = FindObjectOfType<SequenceManager>();
	}

	public Vector3 GetInitialPosition()
	{
		return initialPosition;
	}

	public Quaternion GetInitialRotation()
	{
		return initialRotation;
	}

	public void ReturnToInitial()
	{
		StopAllCoroutines();
		actions.Clear();
		anim.SetBool("Roll_Anim", false);
        anim.SetBool("Walk_Anim", false);
		transform.position = GetInitialPosition();
		transform.rotation = GetInitialRotation();
		sequenceManager.Reset();
		OnAnimationSequenceFinished();
	}

	public void WalkRobot()
	{
		StartCoroutine(WalkForward());
	}

    public IEnumerator WalkForward()
    {
		onceAnim = true;
        anim.SetBool("Walk_Anim", true);
	
		for(int j = 0; j < 1; j++) //2 
		{
			for(int i = 0; i < 5; i++)  //14 //10
        	{
            	transform.Translate(Vector3.forward * Time.fixedDeltaTime * 3.9f); //6.9  //1
            	yield return new WaitForSecondsRealtime(delaySpeed); 
        	}
		}
        
        anim.SetBool("Walk_Anim", false);
		onceAnim = false; 
    }

	private IEnumerator WaitToTurnLeft()
	{
		yield return new WaitForSecondsRealtime(1f);
		anim.SetBool("Roll_Anim", true);
		yield return new WaitForSecondsRealtime(1f);
		Vector3 rotationAdd = new Vector3(0, -90, 0);
		yield return new WaitForSecondsRealtime(1f);
		this.transform.Rotate(rotationAdd);
		anim.SetBool("Roll_Anim", false);
		yield return new WaitForSecondsRealtime(2f);
	}

	private IEnumerator WaitToTurnRight()
	{
		yield return new WaitForSecondsRealtime(1f);
		anim.SetBool("Roll_Anim", true);
		yield return new WaitForSecondsRealtime(1f);
		Vector3 rotationAdd = new Vector3(0, 90, 0);
		yield return new WaitForSecondsRealtime(1f);
		this.transform.Rotate(rotationAdd);
		anim.SetBool("Roll_Anim", false);
		yield return new WaitForSecondsRealtime(2f);
	}

	public IEnumerator StartInstrunction()
	{
		foreach (var coroutine in actions)
		{
			yield return StartCoroutine(coroutine);
		}

		OnAnimationSequenceFinished();
        sequenceManager.Reset();
	}

	public void SetRobotInstruction()
	{
		StartCoroutine(StartInstrunction());
	}

	public void AddInstructionForward()
	{
		actions.Add(WalkForward());
	}

	public void AddInstructionRight()
	{
		actions.Add(WaitToTurnRight());
	}

	public void AddInstructionLeft()
	{
		actions.Add(WaitToTurnLeft());
	}
}
