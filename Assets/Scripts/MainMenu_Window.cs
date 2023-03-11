using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu_Window : MonoBehaviour
{
    private Button Play_btn;
    private Button Quit_btn;
    private void Awake()
    {
        Play_btn = transform.Find("Play_btn").GetComponent<Button>();
        Play_btn.onClick.AddListener(Play_btn_clicked);

        Quit_btn = transform.Find("Quit_btn").GetComponent<Button>();
        Quit_btn.onClick.AddListener(Quit_btn_clicked);
    }

    private void Play_btn_clicked()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);

        Loader.LoadScene(Loader.Scene.Game_Scene);
    }
    private void Quit_btn_clicked()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);

        Application.Quit();
    }

    public void btn_HoverSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonOver);

    }
}
