using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CherryController : MonoBehaviour
{
    public GameObject cherryPrefab; 
    public float spawnInterval = 10f; 
    public float moveSpeed = 20f; 
    public Tilemap nonWalkableTilemap; 
    public float cherryScale = 15f; 

    private Vector3 mapCenter;
    private BoundsInt tilemapBounds;

    private void Start()
    {
        tilemapBounds = nonWalkableTilemap.cellBounds;
        mapCenter = GetMapCenter(); 
        StartCoroutine(SpawnCherry());
    }

    private IEnumerator SpawnCherry(){
        while (true){
            yield return new WaitForSeconds(spawnInterval);

            Vector3 spawnPosition = GetRandomOffTilemapPosition();
            GameObject cherry = Instantiate(cherryPrefab, spawnPosition, Quaternion.identity);
            cherry.transform.localScale = new Vector3(cherryScale, cherryScale, 1);
            Vector3 targetPosition = GetOppositePositionThroughCenter(spawnPosition);
            StartCoroutine(MoveCherry(cherry, targetPosition));
        }
    }

    private Vector3 GetMapCenter(){
        Vector3Int centerGrid = new Vector3Int(
            (tilemapBounds.xMin + tilemapBounds.xMax) / 2,
            (tilemapBounds.yMin + tilemapBounds.yMax) / 2,
            0
        );
        return nonWalkableTilemap.GetCellCenterWorld(centerGrid);
    }

    private Vector3 GetRandomOffTilemapPosition(){
        float spawnX, spawnY;
        int side = Random.Range(0, 4);

        switch (side){
            case 0: 
                spawnX = tilemapBounds.xMin - 5f;
                spawnY = Random.Range(tilemapBounds.yMin, tilemapBounds.yMax);
                break;
            case 1:  
                spawnX = tilemapBounds.xMax + 520f;
                spawnY = Random.Range(tilemapBounds.yMin, tilemapBounds.yMax);
                break;
            case 2: 
                spawnX = Random.Range(tilemapBounds.xMin, tilemapBounds.xMax);
                spawnY = tilemapBounds.yMax + 400f;
                break;
            default: 
                spawnX = Random.Range(tilemapBounds.xMin, tilemapBounds.xMax);
                spawnY = tilemapBounds.yMin - 5f;
                break;
        }

        return new Vector3(spawnX, spawnY, 0);
    }

    private Vector3 GetOppositePositionThroughCenter(Vector3 spawnPosition){
        Vector3 directionToCenter = (mapCenter - spawnPosition).normalized;
        Vector3 oppositePosition = mapCenter + directionToCenter * tilemapBounds.size.magnitude * 9;
        return oppositePosition;
    }

    private IEnumerator MoveCherry(GameObject cherry, Vector3 targetPosition){
        Vector3 startPosition = cherry.transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float totalTime = distance / moveSpeed;

        float elapsedTime = 0f;
        while (elapsedTime < totalTime){
            if (cherry == null){
                yield break;
            }
            cherry.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(cherry);
    }
}
