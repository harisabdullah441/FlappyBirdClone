using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GameOverWindow : MonoBehaviour
{
    private Text scoreText;
    private Text highScoreText;

    private Button retry_btn;
    private Button MainMenu_btn;
    private void Awake()
    {
        scoreText = transform.Find("score_Text").GetComponent<Text>();
        highScoreText = transform.Find("highScore_txt").GetComponent<Text>();
        retry_btn = transform.Find("Retry_btn").GetComponent<Button>();
        retry_btn.onClick.AddListener(Retry_btn_clicked);
        MainMenu_btn = transform.Find("MainMenuBtn").GetComponent<Button>();
        MainMenu_btn.onClick.AddListener(MainMenu_btn_clicked);
     
      
    }

    private void MainMenu_btn_clicked()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        Loader.LoadScene(Loader.Scene.Main_Menu);
      
    }

    private void Retry_btn_clicked()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        Loader.LoadScene(Loader.Scene.Game_Scene);


    }

    public void btn_HoverSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonOver);

    }
   
  

    private void Start()
    {
        Hide_GameOverWindow();
        Bird.GetInstance().OnBirdDie += showScore_onGameOverWindow;
    }
  
    private void showScore_onGameOverWindow(object sender, System.EventArgs e)
    {
        scoreText.text = ScoreWindow.GetInstance().current_score.ToString("0");
        Show_GameOverWindow();

        if (ScoreWindow.GetInstance().current_score>=ScoreHandler.GetHighScore())
        {
            highScoreText.text = "NEW HighScore!!!";
        }else
        {
            highScoreText.text ="HighScore: " +ScoreHandler.GetHighScore().ToString();
        }
    }

    private void Hide_GameOverWindow()
    {
        gameObject.SetActive(false);
    }
    private void Show_GameOverWindow()
    {
        gameObject.SetActive(true);
    }
}



