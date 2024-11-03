using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI bestTimeText;

    void Start()
    {
        // PlayerPrefs.DeleteKey("HighScore");
        // PlayerPrefs.DeleteKey("BestTime");
        LoadHighScore();
        LoadBestTime();       
    }

    private void LoadHighScore(){
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = highScore.ToString();
    }

    private void LoadBestTime(){
        float bestTime = PlayerPrefs.GetFloat("BestTime", 0f);

        if (bestTime == 0f){
            bestTimeText.text = "--:--:--";
        }
        else {
            int minutes = Mathf.FloorToInt(bestTime / 60F);
            int seconds = Mathf.FloorToInt(bestTime % 60F);
            int milliseconds = Mathf.FloorToInt((bestTime * 100F) % 100F);
            bestTimeText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
    }
}
