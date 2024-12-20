using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusicSource; 
    public AudioSource sfxSource;              
    public AudioClip backgroundMusic;          
    public AudioClip walkingSound;            
    public AudioClip pelletEatingSound;   
    public AudioClip scaredMusic;   

    private bool isPlayingWalkingSound = false;

    private void Start()
    {
        PlayBackgroundMusic(); 
    }

    private void PlayBackgroundMusic(){
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    private void PlayScaredMusic(){
        if (backgroundMusicSource.clip != scaredMusic){
            backgroundMusicSource.clip = scaredMusic;
            backgroundMusicSource.Play();
        }
    }

    public void PlayNormalMusic(){
        if (backgroundMusicSource.clip != backgroundMusic){
            backgroundMusicSource.clip = backgroundMusic;
            backgroundMusicSource.Play();
        }
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
}
