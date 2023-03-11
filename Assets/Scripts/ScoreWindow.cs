using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreWindow : MonoBehaviour
{
    public Text scoreText;
    public float current_score=0f;
    private static ScoreWindow insatance;
    public Text highScore;
    private void Awake()
      {
        insatance = this;
        scoreText = transform.Find("Score_Text").GetComponent<Text>();
        highScore = transform.Find("HighScore_txt").GetComponent<Text>();
        
       scoreText.text = current_score.ToString("0");

        highScore.text = "HighScore: " + ScoreHandler.GetHighScore().ToString();
    }

public static ScoreWindow GetInstance()
    { return insatance; }

  
}
