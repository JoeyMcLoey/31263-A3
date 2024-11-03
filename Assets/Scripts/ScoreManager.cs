using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // public TMP_Text scoreText;

    private int score = 0;
    private int highScore = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadHighScore();
        updateScoreUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addPoints(int points){
        score += points;
        updateScoreUI();

        if (score > highScore){
            highScore = score;
            SaveHighScore();
        }
    }

    private void updateScoreUI(){
        scoreText.text = score.ToString();
    }

    private void LoadHighScore(){
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void SaveHighScore(){
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public int GetScore(){
        return score;
    }
}
