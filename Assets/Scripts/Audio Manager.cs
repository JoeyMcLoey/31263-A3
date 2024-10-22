using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusicSource; // Separate audio source for background music
    public AudioSource sfxSource;             // Separate audio source for sound effects (walking, eating)
    public AudioClip backgroundMusic;         // Background music
    public AudioClip walkingSound;            // PacStudent's walking sound
    public AudioClip pelletEatingSound;       // Sound for eating pellets

    private bool isPlayingWalkingSound = false;
    private bool isPlayingPelletSound = false; // New flag to track if pellet sound is playing

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
        if (isMoving && !isPlayingWalkingSound && !isPlayingPelletSound) // Ensure no overlapping sound
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
        if (isPlayingWalkingSound)
        {
            StopWalkingSound(); // Stop walking sound when eating a pellet
        }
        
        if (!isPlayingPelletSound)
        {
            // Play pellet-eating sound once without looping
            sfxSource.PlayOneShot(pelletEatingSound);
            isPlayingPelletSound = true;

            // After the pellet sound is done, resume walking sound if still moving
            Invoke(nameof(ResetPelletSoundFlag), pelletEatingSound.length);
        }
    }

    private void ResetPelletSoundFlag()
    {
        isPlayingPelletSound = false;

        // If PacStudent is still moving, resume the walking sound
        if (isPlayingWalkingSound)
        {
            PlayWalkingSound();
        }
    }
}
