using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Vector3Int gridPosition;
    private Vector2Int lastInput;
    private Vector2Int currentInput;
    private bool isLerping = false;
    private Tilemap nonWalkableTilemap;

    private AudioManager audioManager; // Reference to AudioManager
    public Animator pacStudentAnimator; // Reference to Animator

    void Start()
    {
        nonWalkableTilemap = GameObject.FindWithTag("NonWalkable").GetComponent<Tilemap>();
        gridPosition = new Vector3Int(0, -5, 0);
        SnapToGrid(gridPosition);
        lastInput = Vector2Int.zero;
        currentInput = Vector2Int.zero;

        // Reference to AudioManager
        audioManager = FindFirstObjectByType<AudioManager>();
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
        // Gather input and update lastInput direction
        if (Input.GetKeyDown(KeyCode.W)) lastInput = Vector2Int.up;
        if (Input.GetKeyDown(KeyCode.S)) lastInput = Vector2Int.down;
        if (Input.GetKeyDown(KeyCode.A)) lastInput = Vector2Int.left;
        if (Input.GetKeyDown(KeyCode.D)) lastInput = Vector2Int.right;
    }

    void MovePacStudent()
    {
        // Calculate new target position based on lastInput
        Vector3Int targetPosition = gridPosition + new Vector3Int(lastInput.x, lastInput.y, 0);

        // If lastInput direction is walkable, move PacStudent
        if (IsTileWalkable(targetPosition))
        {
            currentInput = lastInput;
            StartCoroutine(LerpPosition(targetPosition));
        }
        else
        {
            // Try moving in the currentInput direction
            targetPosition = gridPosition + new Vector3Int(currentInput.x, currentInput.y, 0);
            if (IsTileWalkable(targetPosition))
            {
                StartCoroutine(LerpPosition(targetPosition));
            }
        }
    }

    bool IsTileWalkable(Vector3Int position)
    {
        TileBase nonWalkableTile = nonWalkableTilemap.GetTile(position);
        return nonWalkableTile == null;
    }

    IEnumerator LerpPosition(Vector3Int targetPosition)
    {
        isLerping = true;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = nonWalkableTilemap.GetCellCenterWorld(targetPosition);
        float elapsedTime = 0f;
        float totalTime = 1f / moveSpeed;

        // Notify AudioManager that PacStudent is moving
        audioManager.HandlePacStudentMovementAudio(true);

        // Update animation based on direction
        UpdatePacStudentAnimationDirection(targetPosition);

        while (elapsedTime < totalTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SnapToGrid(targetPosition);
        gridPosition = targetPosition;
        isLerping = false;

        if (!HasPendingInput())
        {
            // Notify AudioManager that PacStudent has stopped
            audioManager.HandlePacStudentMovementAudio(false);
        }

        // Check for pellet collision
        CheckForPelletCollision();
    }

    void SnapToGrid(Vector3Int gridPosition)
    {
        Vector3 worldPosition = nonWalkableTilemap.GetCellCenterWorld(gridPosition);
        transform.position = worldPosition;
    }

    void CheckForPelletCollision()
    {
        TileBase tile = nonWalkableTilemap.GetTile(gridPosition);

        // If PacStudent is on a "Walkable" tile (pellet), play the pellet-eating sound
        if (tile != null && nonWalkableTilemap.CompareTag("Walkable"))
        {
            audioManager.PlayPelletEatingSound();
        }
    }

    void UpdatePacStudentAnimationDirection(Vector3Int targetPosition)
    {
        // Update animator parameters based on movement direction
        Vector3 direction = (nonWalkableTilemap.GetCellCenterWorld(targetPosition) - transform.position).normalized;

        if (pacStudentAnimator != null)
        {
            pacStudentAnimator.SetFloat("MoveX", direction.x);
            pacStudentAnimator.SetFloat("MoveY", direction.y);
            pacStudentAnimator.SetBool("isMoving", true);
        }
    }

    bool HasPendingInput()
    {
        return lastInput != Vector2Int.zero || currentInput != Vector2Int.zero;
    }
}
