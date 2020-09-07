using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDMenu : Menu<HUDMenu>
{
    [SerializeField]
    private GameManager gameManager = null;

    [SerializeField]
    private Text timeElapsed = null;
    [SerializeField]
    private Text scoreText = null;
    [SerializeField]
    private Image nextSprite = null;

    private void Start()
    {
        Instantiate<GameManager>(gameManager, transform.position, Quaternion.identity);
    }

    void Update()
    {
        timeElapsed.text = GameManager.Instance.TimeElapsed.ToString("0.00");
        if (GameManager.Instance.TimeElapsed < 1f && scoreText.text != "0")
        {
            print("£");
            scoreText.text = "0";
        }
    }

    public void DisplayNextSprite(Sprite sprite)
    {
        nextSprite.sprite = sprite;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
