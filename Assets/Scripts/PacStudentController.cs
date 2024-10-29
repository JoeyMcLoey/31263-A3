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
    public AudioManager audioManager;
    public Animator pacStudentAnimator;
    public ParticleSystem dustParticle;
    public ScoreManager scoreManager;

    void Start()
    {
        nonWalkableTilemap = GameObject.FindWithTag("NonWalkable").GetComponent<Tilemap>();
        gridPosition = new Vector3Int(0, -5, 0);
        SnapToGrid(gridPosition);
        lastInput = Vector2Int.zero;
        currentInput = Vector2Int.zero;
        audioManager = FindFirstObjectByType<AudioManager>();
        dustParticle.Stop();

        if (scoreManager == null){
            scoreManager = FindFirstObjectByType<ScoreManager>();
        }
    }

    void Update()
    {
        HandleInput();
        if (!isLerping){
            MovePacStudent();
        }
    }

    void HandleInput(){
        if (Input.GetKeyDown(KeyCode.W)) lastInput = Vector2Int.up;
        if (Input.GetKeyDown(KeyCode.S)) lastInput = Vector2Int.down;
        if (Input.GetKeyDown(KeyCode.A)) lastInput = Vector2Int.left;
        if (Input.GetKeyDown(KeyCode.D)) lastInput = Vector2Int.right;
    }

    void MovePacStudent(){
        Vector3Int targetPosition = gridPosition + new Vector3Int(lastInput.x, lastInput.y, 0);

        bool isMoving = false;

        if (IsTileWalkable(targetPosition)){
            currentInput = lastInput;
            isMoving = true;
            StartCoroutine(LerpPosition(targetPosition));
        }
        else {
            targetPosition = gridPosition + new Vector3Int(currentInput.x, currentInput.y, 0);
            if (IsTileWalkable(targetPosition)){
                isMoving = true;
                StartCoroutine(LerpPosition(targetPosition));
            }
        }

        UpdatePacStudentAnimationDirection(isMoving);
        audioManager.HandlePacStudentMovementAudio(isMoving);

        if (isMoving && !dustParticle.isPlaying){
            dustParticle.Play();
        }
        else if (!isMoving && dustParticle.isPlaying){
            dustParticle.Stop();
        }
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

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Pellet")){
            audioManager.PlayPelletEatingSound();
            Destroy(collision.gameObject);
            scoreManager.addPoints(10);
        }

        if (collision.gameObject.CompareTag("Cherry")){
            Destroy(collision.gameObject);
            scoreManager.addPoints(100);
        }

        if (collision.gameObject.CompareTag("PowerPellet")){
            Destroy(collision.gameObject);
        }
    }

    void UpdatePacStudentAnimationDirection(bool isMoving){
        if (pacStudentAnimator != null){
            pacStudentAnimator.SetFloat("MoveX", currentInput.x);
            pacStudentAnimator.SetFloat("MoveY", currentInput.y);
            pacStudentAnimator.SetBool("isMoving", isMoving);
        }
    }
}
