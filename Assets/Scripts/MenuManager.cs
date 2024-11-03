using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public TimeManager timeManager;
    
    public void LoadLevel1(){
        SceneManager.LoadScene("Level1Scene");
    }

    public void ExitGame(){
        if (timeManager != null){
            timeManager.StopTimer();
        }
        SceneManager.LoadScene("StartScene");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
