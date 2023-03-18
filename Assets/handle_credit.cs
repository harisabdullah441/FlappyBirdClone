using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handle_credit : MonoBehaviour
{
    public GameObject credit_screen;
    public GameObject mainMenu_otherUI;

    public void credit_screen_clicked()
    {
        credit_screen.SetActive(true);
        mainMenu_otherUI.SetActive(false);
    }

    public void credit_screen_back_btn()
    {
        credit_screen.SetActive(false);
        mainMenu_otherUI.SetActive(true);
    }
}
