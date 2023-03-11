using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreHandler 

    
{
   public static void Start()
    {
        Bird.GetInstance().OnBirdDie += SetHighScore_Scubscriber;
    }

    private static void SetHighScore_Scubscriber(object sender, System.EventArgs e)
    {
        SetHighScore(ScoreWindow.GetInstance().current_score);
        ScoreWindow.GetInstance().highScore.text = "HighScore: "+GetHighScore().ToString();
    }

    public static float GetHighScore()
    {
        return PlayerPrefs.GetFloat("highScore",0);
    }
    public static void SetHighScore(float score)
    {
      
        float currentHighScore = PlayerPrefs.GetFloat("highScore",0);
        if (score>currentHighScore)
        {
            Debug.Log("SetetHighScore");
            PlayerPrefs.SetFloat("highScore", score);
            PlayerPrefs.Save();
        }
    }

}
