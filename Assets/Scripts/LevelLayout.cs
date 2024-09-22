using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLayout : MonoBehaviour
{
    public GameObject[] tiles;
    public int tileSize = 16;
    int[,] levelMap =
        {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
        };

    // Start is called before the first frame update
    void Start()
    {
        ProduceLevel();
    }

    void ProduceLevel(){
        for (int y = 0; y < levelMap.GetLength(0); y++){
            for (int x = 0; x < levelMap.GetLength(1); x++){
                int i = levelMap[y, x];
                if (i != 0){
                    Vector3 position = new Vector3(x * tileSize, -y * tileSize, 0);
                    Instantiate(tiles[i], position, Quaternion.identity);
                }
            }
        }
        MirrorLevel();
    }

    void MirrorLevel(){
        for (int y = 0; y < levelMap.GetLength(0); y++){
            for (int x = 0; x < levelMap.GetLength(1); x++){
                int tileIndex = levelMap[y, x];
                if (tileIndex != 0){
                    Vector3 posRight = new Vector3((levelMap.GetLength(1) - 1 - x) * tileSize, -y * tileSize, 0);
                    Instantiate(tiles[tileIndex], posRight, Quaternion.Euler(0, 180, 0));
                    
                    Vector3 posBottom = new Vector3(x * tileSize, -(levelMap.GetLength(0) - 1 - y) * tileSize, 0);
                    Instantiate(tiles[tileIndex], posBottom, Quaternion.Euler(180, 0, 0));
                    
                    Vector3 posBottomRight = new Vector3((levelMap.GetLength(1) - 1 - x) * tileSize, -(levelMap.GetLength(0) - 1 - y) * tileSize, 0);
                    Instantiate(tiles[tileIndex], posBottomRight, Quaternion.Euler(180, 180, 0));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
