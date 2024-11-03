using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public int countdownDuration = 3; 
    public PacStudentController pacStudent; 
    public AudioClip walkingStateMusic;     
    public AudioManager audioManager;       
    public TimeManager timeManager;

    private void Start(){
        StartCoroutine(StartRoundCountdown());
    }

    private IEnumerator StartRoundCountdown(){
        pacStudent.enabled = false;

        for (int i = countdownDuration; i > 0; i--){
            countdownText.text = i.ToString();
            countdownText.gameObject.SetActive(true); 
            yield return new WaitForSeconds(1f);    
        }
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);  
        countdownText.gameObject.SetActive(false);
        timeManager.StartTimer();

        pacStudent.enabled = true;
        audioManager.PlayWalkingSound();
    }
}

