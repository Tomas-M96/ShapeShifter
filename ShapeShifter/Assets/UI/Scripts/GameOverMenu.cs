using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : Menu<GameOverMenu>
{
    [SerializeField]
    private Text score = null;
    [SerializeField]
    private Text timeElapsed = null;

    // Start is called before the first frame update
    void Update()
    {
        score.text = GameManager.Instance.Score.ToString();
        print("Final Score: " + GameManager.Instance.Score);
        timeElapsed.text = GameManager.Instance.TimeElapsed.ToString("0.00");
    }

    public void PlayAgainPressed()
    {
        GameOverMenu.Close();
        GameManager.Instance.ResetGame();
        GameManager.Instance.SpawnPlayer();
        GameManager.Instance.ResumeGame();
    }

    public void HomePressed()
    {
        GameManager.Instance.ResetGame();
        MenuManager.Instance.ReturnToMainMenu();
    }
}
