using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highScoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        highScores.highScoreEntries.Sort((h1, h2) => h2.score.CompareTo(h1.score));
        highScoreEntryTransformList = new List<Transform>();
        foreach (var highScoreEntry in highScores.highScoreEntries)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransformList);
        }
    }


    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container,
        List<Transform> transformList)
    {
        float templateHeight = 100f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        String rankString;
        switch (rank)
        {
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
            default:
                rankString = rank + "TH";
                break;
        }

        entryTransform.Find("RankText").GetComponent<Text>().text = rankString;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = highScoreEntry.score.ToString();
        entryTransform.Find("TimeText").GetComponent<Text>().text = (int)Math.Round(highScoreEntry.timePlayed) +" s";

        transformList.Add(entryTransform);
    }

    public static void AddHighScoreEntry(int score, float timePlayed)
    {
        // Create highScore entry
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, timePlayed = timePlayed };
        
        // Get list from PlayerPrefs
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
        
        // Add new entry
        highScores.highScoreEntries.Add(highScoreEntry);
        
        // Save list
        string json = JsonUtility.ToJson(new HighScores { highScoreEntries = highScores.highScoreEntries });
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
    }

    [Serializable]
    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntries;
    }

    [Serializable]
    private class HighScoreEntry
    {
        public int score;
        public float timePlayed;
    }
}