using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public class Score
    {
        private float pointsScore;

        public float PointsScore   
        {
            get { return pointsScore; }
            set { pointsScore = value; }
        }

        public Score(float pointsScore)
        {
            this.pointsScore = pointsScore;
        }

        public void AddScore(float pointsToAdd)
        {
            PointsScore += pointsToAdd;
        }
    }

    [SerializeField] private TextMeshProUGUI textScore;
    ScoreManager.Score score = new ScoreManager.Score(0f);

    private void Awake() 
    {
        textScore = GameObject.Find("TextScore").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddNewScore()
    {
        score.AddScore(10f);
        UpdateUI();
    }

    public void UpdateUI()
    {
        textScore.text = score.PointsScore.ToString();
    }

}
