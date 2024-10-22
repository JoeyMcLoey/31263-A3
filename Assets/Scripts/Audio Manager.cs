using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusicSource;  // Separate audio source for background music
    public AudioSource sfxSource;              // Separate audio source for sound effects (walking, eating)
    public AudioClip backgroundMusic;          // Background music
    public AudioClip walkingSound;             // PacStudent's walking sound
    public AudioClip pelletEatingSound;        // Sound for eating pellets

    private bool isPlayingWalkingSound = false;
    private bool isPlayingPelletSound = false; // Flag to track if pellet sound is playing

    private void Start()
    {
        PlayBackgroundMusic(); // Start background music on loop
    }

    private void PlayBackgroundMusic()
    {
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    public void HandlePacStudentMovementAudio(bool isMoving)
{
    if (isMoving && !isPlayingWalkingSound)
    {
        PlayWalkingSound(); // Start walking sound if moving and not already playing
        isPlayingWalkingSound = true;
    }
    else if (!isMoving && isPlayingWalkingSound)
    {
        StopWalkingSound(); // Stop walking sound when PacStudent stops
        isPlayingWalkingSound = false;
    }
}

    private void PlayWalkingSound()
    {
        sfxSource.clip = walkingSound;
        sfxSource.loop = true; // Ensure the walking sound loops while moving
        sfxSource.Play();
    }

    private void StopWalkingSound()
    {
        sfxSource.Stop(); // Stop the walking sound
    }

    public void PlayPelletEatingSound()
    {
        // Play pellet-eating sound without interrupting the walking sound
        sfxSource.PlayOneShot(pelletEatingSound);
    }


    private void ResetPelletSoundFlag()
    {
        isPlayingPelletSound = false;
    }
}
