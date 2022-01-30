using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [Space]
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text currentScoreText;
    //
    private int highScore = 0;
    private float score = 0;
    private bool recording = false;
    
    public void UpdateHighScoreInfo()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = $"HighScore: {highScore} sec";
    }

    public void StartScoreRecording()
    {
        currentScoreText.text = $"Score: 0 sec";
        score = 0;
        recording = true;
    }

    private void Start()
    {
        UpdateHighScoreInfo();
    }

    void Update()
    {
        if (recording)
        {
            if (!gameController.Death)
            {
                score += Time.deltaTime;
                int scoreInt = Convert.ToInt32(score);
                currentScoreText.text = $"Score: {scoreInt} sec";
                if (scoreInt > highScore)
                {
                    highScoreText.text = $"HighScore: {scoreInt} sec";
                    highScore = scoreInt;
                    PlayerPrefs.SetInt("HighScore", scoreInt);
                }
            }
            else
            {
                recording = false;
                if (Convert.ToInt32(score) > highScore)
                {
                    PlayerPrefs.SetInt("HighScore", Convert.ToInt32(score));
                }
            }
        }
    }
}
