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

        bool isMoving = false;

        // If lastInput direction is walkable, move PacStudent
        if (IsTileWalkable(targetPosition))
        {
            currentInput = lastInput;
            isMoving = true;
            StartCoroutine(LerpPosition(targetPosition));
        }
        else
        {
            // Try moving in the currentInput direction
            targetPosition = gridPosition + new Vector3Int(currentInput.x, currentInput.y, 0);
            if (IsTileWalkable(targetPosition))
            {
                isMoving = true;
                StartCoroutine(LerpPosition(targetPosition));
            }
        }

        // Update the animation based on movement direction
        UpdatePacStudentAnimationDirection(isMoving);

        // Notify AudioManager whether PacStudent is moving or not
        audioManager.HandlePacStudentMovementAudio(isMoving);
    }

    bool IsTileWalkable(Vector3Int position)
    {
        // Check if the tile is walkable (not non-walkable) by checking if it's null
        TileBase nonWalkableTile = nonWalkableTilemap.GetTile(position);
        return nonWalkableTile == null; // Walkable if the tile is null
    }

    IEnumerator LerpPosition(Vector3Int targetPosition)
    {
        isLerping = true;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = nonWalkableTilemap.GetCellCenterWorld(targetPosition);
        float elapsedTime = 0f;
        float totalTime = 1f / moveSpeed;

        while (elapsedTime < totalTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SnapToGrid(targetPosition);
        gridPosition = targetPosition;
        isLerping = false;
    }

    void SnapToGrid(Vector3Int gridPosition)
    {
        Vector3 worldPosition = nonWalkableTilemap.GetCellCenterWorld(gridPosition);
        transform.position = worldPosition;
    }

    // Handle collision with pellets
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pellet")) // Ensure the pellet has the "Pellet" tag
        {
            // Play the pellet-eating sound
            audioManager.PlayPelletEatingSound();

            // Destroy the pellet on collision
            Destroy(collision.gameObject);
        }
    }

    void UpdatePacStudentAnimationDirection(bool isMoving)
    {
        if (pacStudentAnimator != null)
        {
            // Set the direction parameters for the animator based on currentInput
            pacStudentAnimator.SetFloat("MoveX", currentInput.x);
            pacStudentAnimator.SetFloat("MoveY", currentInput.y);

            // Set the isMoving parameter to control animation
            pacStudentAnimator.SetBool("isMoving", isMoving);
        }
    }
}
