using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI levelDifficultyText;
    [SerializeField]
    private TextMeshProUGUI mistakesText;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        if (scoreText)
            scoreText.text = GridController.Instance.LevelController.ScoreText.text;
        if (mistakesText)
            mistakesText.text = GridController.Instance.LevelController.MistakesText.text;
        if (timeText)
            timeText.text = GridController.Instance.LevelController.GameplayTimeText.text;
        if (levelDifficultyText)
            levelDifficultyText.text = GridController.Instance.LevelController.LevelDifficultyText.text;
    }

}
