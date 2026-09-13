using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Transform scoreContainer;
    [SerializeField] private GameObject scoreRowPrefab;

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        // Remove existing rows
        foreach (Transform child in scoreContainer)
        {
            Destroy(child.gameObject);
        }

        // Get scores
        var scores = LeaderboardManager.Instance.GetScores();

        // Create a row for each score
        for (int i = 0; i < scores.Count; i++)
        {
            HighScoreEntry score = scores[i];

            GameObject row = Instantiate(scoreRowPrefab, scoreContainer);

            TMP_Text[] texts = row.GetComponentsInChildren<TMP_Text>();

            // Expected:
            // texts[0] = position
            // texts[1] = player name
            // texts[2] = score

            texts[0].text = $"{i + 1}";
            texts[1].text = score.name;
            texts[2].text = score.score.ToString();
        }
    }
}