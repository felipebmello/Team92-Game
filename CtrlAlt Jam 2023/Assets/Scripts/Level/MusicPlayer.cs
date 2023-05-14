using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private AudioClip mainMenuClip;
    [SerializeField] private AudioClip level1Clip;
    [SerializeField] private AudioClip level2Clip;
    [SerializeField]private AudioClip level3Clip;

    [SerializeField] [Range(0f, 1f)] float musicVolume = 0.6f;
    public static MusicPlayer Instance { get; private set; }
    private void Awake() 
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = musicVolume;
        audioSource.Play();
        
    }

    internal void SetVolume(float volume)
    {
        musicVolume = volume;
    }
}
