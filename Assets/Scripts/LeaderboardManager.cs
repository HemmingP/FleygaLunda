using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class HighScoreEntry
{
    public string name;
    public int score;

    public HighScoreEntry(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}

[Serializable]
public class HighScoreData
{
    public List<HighScoreEntry> scores = new();
}

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }

    [SerializeField] private int maxScores = 10;

    private HighScoreData data = new();

    private string FilePath =>
        Path.Combine(Application.persistentDataPath, "highscores.json");

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public void Highscores()
    {
        var scores = data.scores;

        for (int i = 0; i < scores.Count; i++)
        {
            Debug.Log($"{i + 1}. {scores[i].name} - {scores[i].score}");
        }
    }

    public void AddScore(string playerName, int score)
    {
        data.scores.Add(new HighScoreEntry(playerName, score));

        data.scores.Sort((a, b) => b.score.CompareTo(a.score));

        if (data.scores.Count > maxScores)
            data.scores.RemoveRange(maxScores, data.scores.Count - maxScores);

        Save();
    }

    public bool IsHighScore(int score)
    {
        if (data.scores.Count < maxScores)
            return true;

        return score > data.scores[^1].score;
    }

    public List<HighScoreEntry> GetScores()
    {
        return data.scores;
    }

    private void Save()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(FilePath, json);
    }

    private void Load()
    {
        if (!File.Exists(FilePath))
        {
            data = new HighScoreData();
            return;
        }

        try
        {
            string json = File.ReadAllText(FilePath);
            data = JsonUtility.FromJson<HighScoreData>(json);

            if (data == null)
                data = new HighScoreData();

            if (data.scores == null)
                data.scores = new List<HighScoreEntry>();
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Could not load highscores: {e.Message}");
            data = new HighScoreData();
        }
    }
}