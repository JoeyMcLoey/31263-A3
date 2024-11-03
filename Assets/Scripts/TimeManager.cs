using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f;
    private bool timerActive = false;

    void Start()
    {
        elapsedTime = 0f;
        LoadBestTime();
        UpdateTimerUI();
    }

    void Update()
    {
        if (timerActive){
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    public void StartTimer(){
        elapsedTime = 0f;
        timerActive = true;
    }

    public void StopTimer(){
        timerActive = false;
                
        float bestTime = PlayerPrefs.GetFloat("BestTime", 0f);

        if (elapsedTime > 0 && (bestTime == 0 || elapsedTime < bestTime)){
            PlayerPrefs.SetFloat("BestTime", elapsedTime);
            PlayerPrefs.Save();
        }
    }

    void UpdateTimerUI(){
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100F) % 100F);
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    private void LoadBestTime(){
        float bestTime = PlayerPrefs.GetFloat("BestTime", 0f);
    }

}
