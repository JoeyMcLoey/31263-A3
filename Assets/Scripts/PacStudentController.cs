using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 2f;            // Movement speed
    private Vector3Int gridPosition;        // Current grid position of PacStudent
    private Vector2Int lastInput;           // Last input from player
    private bool isLerping = false;         // Is PacStudent currently moving
    private Tilemap nonWalkableTilemap;     // Reference to the Non-Walkable Tilemap (Walls)

    void Start()
    {
        // Find the Non-Walkable Tilemap (Walls) in the scene
        nonWalkableTilemap = GameObject.FindWithTag("NonWalkable").GetComponent<Tilemap>();

        // Log the tilemap's bounds to understand the positioning
        Debug.Log("Non-Walkable Tilemap Cell Bounds: " + nonWalkableTilemap.cellBounds);

        // Set PacStudent's initial grid position, adjusted to align with the walkable areas
        gridPosition = new Vector3Int(0, -5, 0);  // Adjust based on alignment testing

        // Snap PacStudent's world position to the center of the grid cell
        SnapToGrid(gridPosition);

        // Initialize input vectors
        lastInput = Vector2Int.zero;

        Debug.Log("Starting grid position: " + gridPosition);
        Debug.Log("Starting world position: " + transform.position);
    }

    void Update()
    {
        HandleInput();

        if (!isLerping)
        {
            MovePacStudent();
        }
    }

    void HandleInput()
    {
        // Capture player input
        if (Input.GetKeyDown(KeyCode.W)) lastInput = Vector2Int.up;
        if (Input.GetKeyDown(KeyCode.S)) lastInput = Vector2Int.down;
        if (Input.GetKeyDown(KeyCode.A)) lastInput = Vector2Int.left;
        if (Input.GetKeyDown(KeyCode.D)) lastInput = Vector2Int.right;
    }

    void MovePacStudent()
    {
        // Calculate the new target grid position based on input
        Vector3Int targetPosition = gridPosition + new Vector3Int(lastInput.x, lastInput.y, 0);

        // Ensure target position is within the bounds of the tilemap (optional)
        if (IsPositionOutOfBounds(targetPosition))
        {
            Debug.LogWarning("Target position out of bounds: " + targetPosition);
            return;
        }

        // Check if the target tile is walkable (not part of the non-walkable tilemap)
        if (IsTileWalkable(targetPosition))
        {
            Debug.Log("Moving to target position: " + targetPosition);
            StartCoroutine(LerpPosition(targetPosition));  // Start the movement
        }
        else
        {
            Debug.Log("Target position not walkable (wall or obstacle): " + targetPosition);
        }
    }

    bool IsPositionOutOfBounds(Vector3Int position)
    {
        // Use tilemap bounds for out-of-bounds checking (optional, depending on the map size)
        BoundsInt bounds = nonWalkableTilemap.cellBounds;

        // Check if the position is outside the tilemap bounds
        return position.x < bounds.xMin || position.x >= bounds.xMax || 
               position.y < bounds.yMin || position.y >= bounds.yMax;
    }

    bool IsTileWalkable(Vector3Int position)
    {
        // Check if the tile exists in the non-walkable tilemap (Walls)
        TileBase nonWalkableTile = nonWalkableTilemap.GetTile(position);

        // If there's no tile in the non-walkable tilemap, it's walkable
        return nonWalkableTile == null;
    }

    IEnumerator LerpPosition(Vector3Int targetPosition)
    {
        isLerping = true;

        // Get the world position corresponding to the target grid position
        Vector3 startPosition = transform.position;
        Vector3 endPosition = nonWalkableTilemap.GetCellCenterWorld(targetPosition);  // Snap to center of grid cell

        Debug.Log("Start Position: " + startPosition + ", End Position: " + endPosition);

        float elapsedTime = 0f;
        float totalTime = 1f / moveSpeed;

        while (elapsedTime < totalTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Snap to the target grid position once lerping is complete
        SnapToGrid(targetPosition);

        // Update grid position and finalize movement
        gridPosition = targetPosition;
        isLerping = false;

        Debug.Log("PacStudent new grid position: " + gridPosition);
    }

    // This function ensures that PacStudent snaps perfectly to the center of the grid
    void SnapToGrid(Vector3Int gridPosition)
    {
        // Use tilemap.GetCellCenterWorld() to perfectly center PacStudent on the tile
        Vector3 worldPosition = nonWalkableTilemap.GetCellCenterWorld(gridPosition);
        transform.position = worldPosition;
    }
}
