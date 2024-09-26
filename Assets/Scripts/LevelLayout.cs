using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** NOTE


I was fiddling around with levelLayout for 100% but im happy to not submit it 
for the final submission. For 85%, just make sure to untick the LevelLayout
script component on the LevelLayout gameObject


**/

public class LevelLayout : MonoBehaviour
{
    public GameObject[] tiles;
    public float tileSize = 1f;
    GameObject parentObject;
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

    void ProduceLevel() {
        parentObject = new GameObject("LevelParent"); 

        for (int y = 0; y < levelMap.GetLength(0); y++) {
            for (int x = 0; x < levelMap.GetLength(1); x++) {
                int i = levelMap[y, x];
                if (i != 0) {
                    Vector3 position = new Vector3(x * 1f, -y * 1f, 0); 
                    GameObject tile = Instantiate(tiles[i], position, Quaternion.identity);
                    tile.transform.parent = parentObject.transform;
                }
            }
        }

        GameObject topRight = Instantiate(parentObject, new Vector3(levelMap.GetLength(1) * 1f, 0, 0), Quaternion.identity);
        topRight.transform.localScale = new Vector3(-1, 1, 1); 
        topRight.transform.position += new Vector3(levelMap.GetLength(1) * 1f, 0, 0);

        GameObject bottomLeft = Instantiate(parentObject, new Vector3(0, -levelMap.GetLength(0) * 1f, 0), Quaternion.identity);
        bottomLeft.transform.localScale = new Vector3(1, -1, 1); 
        bottomLeft.transform.position += new Vector3(0, -(levelMap.GetLength(1)) * 1f, 0);

        GameObject bottomRight = Instantiate(topRight, new Vector3(levelMap.GetLength(1) * 1f, -levelMap.GetLength(0) * 1f, 0), Quaternion.identity);
        bottomRight.transform.localScale = new Vector3(-1, -1, 1);
        bottomRight.transform.position += new Vector3(levelMap.GetLength(1) * 1f, -(levelMap.GetLength(1)) * 1f, 0);
    }



    // Update is called once per frame
    void Update() {
        
    }
}
