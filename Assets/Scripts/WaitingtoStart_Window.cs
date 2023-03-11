using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingtoStart_Window : MonoBehaviour
{
    private void Start()
    {
        Bird.GetInstance().OnPlayingStarted += HideStarting_text;
        Show();
    }

    private void HideStarting_text(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
