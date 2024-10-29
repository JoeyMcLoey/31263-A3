using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusicSource; 
    public AudioSource sfxSource;              
    public AudioClip backgroundMusic;          
    public AudioClip walkingSound;            
    public AudioClip pelletEatingSound;      

    private bool isPlayingWalkingSound = false;
    private bool isPlayingPelletSound = false; 

    private void Start()
    {
        PlayBackgroundMusic(); 
    }

    private void PlayBackgroundMusic(){
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    public void HandlePacStudentMovementAudio(bool isMoving){
        if (isMoving && !isPlayingWalkingSound){
            PlayWalkingSound(); 
            isPlayingWalkingSound = true;
        }
        else if (!isMoving && isPlayingWalkingSound)    {
            StopWalkingSound();
            isPlayingWalkingSound = false;
        }
    }

    public void PlayWalkingSound(){
        sfxSource.clip = walkingSound;
        sfxSource.loop = true;
        sfxSource.Play();
    }

    private void StopWalkingSound(){
        sfxSource.Stop(); 
    }

    public void PlayPelletEatingSound(){
        sfxSource.PlayOneShot(pelletEatingSound);
    }

    private void ResetPelletSoundFlag(){
        isPlayingPelletSound = false;
    }
}
