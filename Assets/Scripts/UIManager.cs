using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RobotController robotController;
    [SerializeField] private Button buttonGo;
    private bool waitForNextClick;
    
    private void Awake() 
    {
        robotController = GameObject.FindObjectOfType<RobotController>();
        buttonGo = GameObject.Find("CanvasUI/PanelCommands/Button_GO").GetComponent<Button>();
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
        if (!waitForNextClick)
        {
            waitForNextClick = true;
            StartCoroutine(WaitForNextInstruction());
        }
    }

    private IEnumerator WaitForNextInstruction()
    {
        buttonGo.interactable = false;
        robotController.SetRobotInstruction();
        yield return new WaitForSecondsRealtime(2f);
        waitForNextClick = false;
        EnableButtonGo();
    }

    private void EnableButtonGo()
    {
        buttonGo.interactable = true;
    }
}
