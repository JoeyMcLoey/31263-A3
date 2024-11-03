using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public PacStudentController pacStudent; 
    public TimeManager timeManager;
    public ScoreManager scoreManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameOverText.gameObject.SetActive(false);
    }

    public void GameOver(){
        gameOverText.gameObject.SetActive(true);
        StopMovement();
        timeManager.StopTimer();
        UpdateScoreAndTime();
        StartCoroutine(BackToStart());
    }

    private void StopMovement(){
        pacStudent.enabled = false;
    }

    private void UpdateScoreAndTime(){
        int currScore = scoreManager.GetScore();
        float currTime = timeManager.GetElapsedTime();

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (currScore > highScore || (currScore == highScore && currTime < bestTime)){
            PlayerPrefs.SetInt("HighScore", currScore);
            PlayerPrefs.SetFloat("BestTime", currTime);
            PlayerPrefs.Save();
        }
    }

    private IEnumerator BackToStart(){
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("StartScene");
    }
}
