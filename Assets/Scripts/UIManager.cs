using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RobotController robotController;
    [SerializeField] private Button buttonGo, buttonLeft, buttonRight, buttonForward;
    private bool waitForNextClick;
    
    private void Awake() 
    {
        robotController = GameObject.FindObjectOfType<RobotController>();
        buttonGo = GameObject.Find("CanvasUI/PanelCommands/Button_GO").GetComponent<Button>();
        buttonLeft = GameObject.Find("CanvasUI/PanelCommands/Button_Left").GetComponent<Button>();
        buttonRight = GameObject.Find("CanvasUI/PanelCommands/Button_Right").GetComponent<Button>();
        buttonForward = GameObject.Find("CanvasUI/PanelCommands/Button_Forward").GetComponent<Button>();
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
        DisableButtonsUI();
        robotController.SetRobotInstruction();
        yield return new WaitForSecondsRealtime(2f);
        waitForNextClick = false;
    }

    private void DisableButtonsUI()
    {
        buttonGo.interactable = false;
        buttonForward .interactable = false;
        buttonLeft.interactable = false;
        buttonRight.interactable = false;
    }

    private void EnableButtonsUI()
    {
        buttonGo.interactable = true;
        buttonForward .interactable = true;
        buttonLeft.interactable = true;
        buttonRight.interactable = true;
    }
   
    private void OnEnable()
    {
        RobotController.OnAnimationSequenceFinished += EnableButtonsUI;
    }

    private void OnDisable() 
    {
        RobotController.OnAnimationSequenceFinished -= EnableButtonsUI;
    }
}
