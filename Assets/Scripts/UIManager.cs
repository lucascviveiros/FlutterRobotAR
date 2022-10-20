using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RobotController robotController;
    
    private void Awake() 
    {
        robotController = GameObject.FindObjectOfType<RobotController>();
    }
    
    public void ButtonForwardClick()
    {
        robotController.AddInstructionForward();
    }

    public void ButtonRigthClick()
    {
        robotController.AddInstructionRight();
    }

    public void ButtonLeftClick()
    {
        robotController.AddInstructionLeft();
    }

    public void ButtonGoClick()
    {
        robotController.SetRobotInstruction();
    }

}
