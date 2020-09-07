using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu<MainMenu>
{
    [SerializeField]
    private GameObject instructionPopup = null;

    public void PlayPressed()
    {
        HUDMenu.Open();
        if (GameManager.Instance != null && GameManager.Instance.IsPaused)
        {
            GameManager.Instance.ResumeGame();
        }
    }

    public void HighScoresPressed()
    {
        HighScoresMenu.Open();
    }

    public void HowToPlayPressed()
    {
        instructionPopup.SetActive(true);
    }

    public void ClosePopup()
    {
        instructionPopup.SetActive(false);
    }
}
