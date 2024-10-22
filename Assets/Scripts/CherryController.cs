using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public GameObject cherryPrefab;  // Reference to the cherry prefab
    public float spawnInterval = 10f;  // Spawn interval for cherries
    public float moveSpeed = 3f;  // Speed at which cherry moves
    private Vector2Int levelBounds;  // Level bounds for cherry spawning and movement

    private void Start()
    {
        levelBounds = new Vector2Int(10, 10);  // Set level bounds based on your map size
        StartCoroutine(SpawnCherry());
    }

    private IEnumerator SpawnCherry()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Spawn cherry at a random position outside the camera view
            Vector3 spawnPosition = GetRandomOffScreenPosition();
            GameObject cherry = Instantiate(cherryPrefab, spawnPosition, Quaternion.identity);

            // Move the cherry towards the center of the level
            Vector3 targetPosition = Vector3.zero;  // Center of the level
            StartCoroutine(MoveCherry(cherry, targetPosition));
        }
    }

    private Vector3 GetRandomOffScreenPosition()
    {
        // Random position outside the camera view (just outside the boundaries)
        float x = Random.Range(-levelBounds.x * 1.5f, levelBounds.x * 1.5f);
        float y = Random.Range(-levelBounds.y * 1.5f, levelBounds.y * 1.5f);
        return new Vector3(x, y, 0);
    }

    private IEnumerator MoveCherry(GameObject cherry, Vector3 targetPosition)
    {
        Vector3 startPosition = cherry.transform.position;
        float lerpTime = 0f;

        while (lerpTime < 1f)
        {
            lerpTime += Time.deltaTime * moveSpeed;
            cherry.transform.position = Vector3.Lerp(startPosition, targetPosition, lerpTime);
            yield return null;
        }

        // Destroy the cherry once it reaches the other side of the level
        Destroy(cherry);
    }
}
