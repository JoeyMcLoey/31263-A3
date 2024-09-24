using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMovement : MonoBehaviour
{
    public AudioSource movementAudioSource;
    public AudioClip movementClip;

    public float speed = 2f;
    private Vector3[] pathPoints = new Vector3[4];

    private int currentTarget = 0;
    private Animator animator;

    void Start()
    { 
        pathPoints[0] = new Vector3(6, -1, 0);
        pathPoints[1] = new Vector3(6, -5, 0);
        pathPoints[2] = new Vector3(1, -5, 0);
        pathPoints[3] = new Vector3(1, -1, 0);

        movementAudioSource.clip = movementClip;
        movementAudioSource.loop = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveAlongPath();
    }

    private void MoveAlongPath(){
        if (Vector3.Distance(transform.position, pathPoints[currentTarget]) < 0.1f){
            currentTarget = (currentTarget + 1) % pathPoints.Length; 
        }

        Vector3 moveDirection = Vector3.MoveTowards(transform.position, pathPoints[currentTarget], speed * Time.deltaTime);
        transform.position = moveDirection;

        Vector3 direction = (pathPoints[currentTarget] - transform.position).normalized;
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
        animator.SetBool("isMoving", true);

        if (!movementAudioSource.isPlaying && speed > 0f){
            movementAudioSource.Play();
        }

    }
}
