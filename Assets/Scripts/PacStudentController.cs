using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 2f;            
    private Vector3Int gridPosition;        
    private Vector2Int lastInput;           
    private bool isLerping = false;        
    private Tilemap nonWalkableTilemap;     

    void Start()
    {
        nonWalkableTilemap = GameObject.FindWithTag("NonWalkable").GetComponent<Tilemap>();
        gridPosition = new Vector3Int(0, -5, 0); 
        SnapToGrid(gridPosition);
        lastInput = Vector2Int.zero;
    }

    void Update()
    {
        HandleInput();

        if (!isLerping){
            MovePacStudent();
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) lastInput = Vector2Int.up;
        if (Input.GetKeyDown(KeyCode.S)) lastInput = Vector2Int.down;
        if (Input.GetKeyDown(KeyCode.A)) lastInput = Vector2Int.left;
        if (Input.GetKeyDown(KeyCode.D)) lastInput = Vector2Int.right;
    }

    void MovePacStudent(){
        Vector3Int targetPosition = gridPosition + new Vector3Int(lastInput.x, lastInput.y, 0);

        if (IsPositionOutOfBounds(targetPosition)){
            Debug.LogWarning("Target position out of bounds: " + targetPosition);
            return;
        }

        if (IsTileWalkable(targetPosition)){
            Debug.Log("Moving to target position: " + targetPosition);
            StartCoroutine(LerpPosition(targetPosition)); 
        }
        else {
            Debug.Log("Target position not walkable (wall or obstacle): " + targetPosition);
        }
    }

    bool IsPositionOutOfBounds(Vector3Int position){
        BoundsInt bounds = nonWalkableTilemap.cellBounds;

        return position.x < bounds.xMin || position.x >= bounds.xMax || 
               position.y < bounds.yMin || position.y >= bounds.yMax;
    }

    bool IsTileWalkable(Vector3Int position){
        TileBase nonWalkableTile = nonWalkableTilemap.GetTile(position);
        return nonWalkableTile == null;
    }

    IEnumerator LerpPosition(Vector3Int targetPosition){
        isLerping = true;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = nonWalkableTilemap.GetCellCenterWorld(targetPosition);

        float elapsedTime = 0f;
        float totalTime = 1f / moveSpeed;

        while (elapsedTime < totalTime){
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SnapToGrid(targetPosition);
        gridPosition = targetPosition;
        isLerping = false;
    }

    void SnapToGrid(Vector3Int gridPosition){
        Vector3 worldPosition = nonWalkableTilemap.GetCellCenterWorld(gridPosition);
        transform.position = worldPosition;
    }
}
