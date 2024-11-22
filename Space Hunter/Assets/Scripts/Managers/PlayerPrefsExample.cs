using TMPro;
using UnityEngine;

public class PlayerConfigurationSaver : MonoBehaviour
{
    public void UpdateScoreUI(TextMeshProUGUI scoreTextUI, int currentScore)
    {
        scoreTextUI.text = $"SCORE: {currentScore}";
    }

    public void AddScore(int value, TextMeshProUGUI scoreTextUI, int currentScore)
    {
        currentScore += value;
        UpdateScoreUI(scoreTextUI, currentScore);
    }

    public void CheckAndSaveHighScore(string HighScoreKey, TextMeshProUGUI highScoreTextUI, int currentScore, int HighScore)
    {
        if (currentScore > HighScore)
        {
            HighScore = currentScore;
            PlayerPrefs.SetInt(HighScoreKey, HighScore);
            PlayerPrefs.Save();
            highScoreTextUI.text = $"High Score: {HighScore}";
        }
        else
        {
            highScoreTextUI.text = $"High Score: {HighScore}";
        }
    }

    public void ResetHighScore(string HighScoreKey)
    {
        PlayerPrefs.SetInt(HighScoreKey, 0);
        PlayerPrefs.Save();
    }
}
