using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoresMenu : Menu<HighScoresMenu>
{

    [SerializeField]
    private Transform content;
    [SerializeField]
    private HighScoreItem leaderboardItem;

    private void Start()
    {
        ReadFile();
    }

    private void ReadFile()
    {
        JSONLoader jsonLoader = new JSONLoader();

        Leaderboard leaderboard = jsonLoader.ReadJson();

        PopulateTable(leaderboard);
    }

    private void PopulateTable(Leaderboard leaderboard)
    {
        foreach (ScoreData item in leaderboard.leaderboard)
        {
            leaderboardItem.name.text = item.name;
            leaderboardItem.score.text = item.score.ToString();
            leaderboardItem.time.text = item.timeElapsed.ToString("0.00");
            Instantiate<HighScoreItem>(leaderboardItem, content);
        }
    }
}
