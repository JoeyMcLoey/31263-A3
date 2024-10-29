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
    
    // Start is called before the first frame update
    void Start()
    {
        updateScoreUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addPoints(int points){
        score += points;
        updateScoreUI();
    }

    private void updateScoreUI(){
        scoreText.text = score.ToString();
    }
}
