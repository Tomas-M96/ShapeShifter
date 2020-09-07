using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class JSONLoader
{
    [SerializeField]
    private ScoreData scoreData = new ScoreData();

    public void SaveJsonToFile(string newName, int newScore, float timeElapsed)
    {
        //Get the current leaderboard data
        Leaderboard currentData = ReadJson();

        //Create a new scoredata
        ScoreData scoreData = new ScoreData
        {
            name = newName,
            score = newScore,
            timeElapsed = float.Parse(timeElapsed.ToString("0.00"))
        };

        //Add the scoredata to the leaderboard
        currentData.leaderboard.Add(scoreData);

        //Convert the new leaderboard object to json
        string json = JsonUtility.ToJson(currentData);

        //Save the json to the file
        File.WriteAllText(Application.dataPath + "/LeaderboardData/ScoreData.json", json);
        Debug.Log(json);
    }

    public Leaderboard ReadJson()
    {
        //get the content from the json file
        string content = File.ReadAllText(Application.dataPath + "/LeaderboardData/ScoreData.json");
        //Convert from json to leaderboard object
        Leaderboard newJson = JsonUtility.FromJson<Leaderboard>(content);
        //Return the object
        return newJson;
    }
}

[System.Serializable]
public class ScoreData
{
    public string name;
    public int score;
    public float timeElapsed;
}

public class Leaderboard
{
    public List<ScoreData> leaderboard;
}
