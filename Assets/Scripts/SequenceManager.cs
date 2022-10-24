using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SequenceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sequenceText;
    private char upArrow = '\u25B2';
    private char leftArrow = '\u25C4';
    private char rightArrow = '\u25BA';
    private string sequence = "";

    private void Awake() 
    {
        sequenceText = GameObject.Find("TextSequence").GetComponent<TextMeshProUGUI>();    
    }
 
    public void Up()
    {
        sequence += upArrow.ToString() + " ";
        Show();
    }

    public void Right()
    {
        sequence += rightArrow.ToString() + " ";
        Show();
    }

    public void Left()
    {
        sequence += leftArrow.ToString() + " ";
        Show();
    }

    public void Show()
    {
        sequenceText.text = sequence;
    }

    public void Go()
    {
        sequenceText.color = new Color32(255, 128, 0, 255);
    }

    public void Reset()
    {
        sequence = "";
        sequenceText.text = sequence;
    }
}
