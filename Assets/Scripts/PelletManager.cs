using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletManager : MonoBehaviour
{
    private int totalPellets;
    private int pelletsEaten;
    public GameOverManager gameOverManager;
    
    // Start is called before the first frame update
    void Start()
    {
        totalPellets = GameObject.FindGameObjectsWithTag("Pellet").Length - 1;
    }

    public void PelletEaten(){
        pelletsEaten++;

        if (pelletsEaten >= totalPellets){
            gameOverManager.GameOver();
        }
    }
}
