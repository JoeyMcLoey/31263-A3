using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMovement : MonoBehaviour
{
    public AudioSource movementAudioSource;
    public AudioClip movementClip;

    public float speed = 2f;
    private Vector3[] pathPoints = new Vector3[4];

    private int currTarget = 0;
    private float moveProgress = 0f;
    private Vector3 startPos;
    private Animator animator;

    void Start()
    { 
        pathPoints[0] = new Vector3(6, -1, 0);
        pathPoints[1] = new Vector3(6, -5, 0);
        pathPoints[2] = new Vector3(1, -5, 0);
        pathPoints[3] = new Vector3(1, -1, 0);

        startPos = transform.position;
        movementAudioSource.clip = movementClip;
        movementAudioSource.loop = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveAlongPath();
    }

    private void MoveAlongPath(){
        if (moveProgress < 1f){
            moveProgress += (speed / Vector3.Distance(startPos, pathPoints[currTarget])) * Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, pathPoints[currTarget], moveProgress);

            Vector3 direction = (pathPoints[currTarget] - transform.position).normalized;
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
            animator.SetBool("isMoving", true);

            if (!movementAudioSource.isPlaying && speed > 0f){
                movementAudioSource.Play();
            }
        }
        else {
            moveProgress = 0f;
            startPos = pathPoints[currTarget];
            currTarget = (currTarget + 1)% pathPoints.Length;
        }        
    }
}
