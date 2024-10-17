using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip introMusic;
    public AudioClip normalState;

    private void Start()
    {
        PlayMusic(introMusic, true);
        // StartCoroutine(PlayNormalBackgroundMusic());
    }

    private void PlayMusic(AudioClip clip, bool loop)
    {
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    // private System.Collections.IEnumerator PlayNormalBackgroundMusic()
    // {
    //     yield return new WaitForSeconds(introMusic.length);

    //     PlayMusic(normalState, true);
    // }
}
