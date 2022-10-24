using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Linq;

public class RobotController : MonoBehaviour
{
    private Vector3 rot = Vector3.zero;
	private float rotSpeed = 0f;
	private Animator anim;
    private float delaySpeed = 0.33f;
	private bool onceAnim;
	private int totalActions = 0;
	private List<IEnumerator> actions = new List<IEnumerator>();
	private Vector3 initialPosition;
	private Quaternion initialRotation;

	void Awake()
	{		
		anim = gameObject.GetComponent<Animator>();	
		initialPosition = transform.position;
		initialRotation = transform.rotation;

		Debug.Log("Get Initial Position:" + initialPosition.ToString());
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
		actions.Clear(); //Clearing Coroutines from list
		anim.SetBool("Roll_Anim", false);
        anim.SetBool("Walk_Anim", false);
		transform.position = GetInitialPosition();
		transform.rotation = GetInitialRotation();
	}

	void Update()
	{
		/*
		if (Input.GetKey(KeyCode.W) && !onceAnim)
		{
			StartCoroutine(WalkForward());
		}*/
	}

	public void WalkRobot()
	{
		StartCoroutine(WalkForward());
	}

    public IEnumerator WalkForward()
    {
		onceAnim = true;
        anim.SetBool("Walk_Anim", true);
	
		for(int j = 0; j < 2; j++) //2
		{
			for(int i = 0; i < 10; i++)  //14
        	{
            	transform.Translate(Vector3.forward * Time.fixedDeltaTime * 1f); //6.9
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
		yield return new WaitForSecondsRealtime(1f);
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
		yield return new WaitForSecondsRealtime(1f);
	}

	public IEnumerator StartInstrunction()
	{
		foreach (var coroutine in actions)
		{
			yield return StartCoroutine(coroutine);
		}					
	}

	public void SetRobotInstruction()
	{
		StartCoroutine(StartInstrunction());
	}

	public void AddInstructionForward()
	{
		//Debug.Log("Add Pra frente");
		actions.Add(WalkForward());
	}

	public void AddInstructionRight()
	{
		//Debug.Log("Add Pra diretira");
		actions.Add(WaitToTurnRight());
	}

	public void AddInstructionLeft()
	{
		//Debug.Log("Add Pra esquerda");
		actions.Add(WaitToTurnLeft());
	}

	private void CheckKey()
	{
		// Walk
		if (Input.GetKey(KeyCode.W))
		{
			anim.SetBool("Walk_Anim", true);
			//speedPercent += _agent.velocity.magnitude / _agent.speed + 1;
            transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical"));
            Debug.Log("Vertical Axis: " + Input.GetAxis("Vertical"));
		}
		else if (Input.GetKeyUp(KeyCode.W))
		{
			anim.SetBool("Walk_Anim", false);
			//anim.SetFloat("Walk", 0);

		}

		// Rotate Left
		if (Input.GetKey(KeyCode.A))
		{
			rot[1] -= rotSpeed * Time.fixedDeltaTime;
		}

		// Rotate Right
		if (Input.GetKey(KeyCode.D))
		{
			rot[1] += rotSpeed * Time.fixedDeltaTime;
		}


		// Roll
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (anim.GetBool("Roll_Anim"))
			{
				anim.SetBool("Roll_Anim", false);
			}
			else
			{
				anim.SetBool("Roll_Anim", true);
			}
		}

		// Close
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			if (!anim.GetBool("Open_Anim"))
			{
				anim.SetBool("Open_Anim", true);
			}
			else
			{
				anim.SetBool("Open_Anim", false);
			}
		}
	}



}
