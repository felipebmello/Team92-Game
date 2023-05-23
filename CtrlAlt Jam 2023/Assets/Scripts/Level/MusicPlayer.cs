using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }
    private AudioSource audioSource;
    public float fadeDuration = 1.0f;
    //[SerializeField] private AudioClip mainMenuClip;
    [SerializeField] private AudioClip level1Clip;
    [SerializeField] private AudioClip level2Clip;
    //[SerializeField]private AudioClip level3Clip;

    [SerializeField] [Range(0f, 1f)] float musicVolume = 0.6f;
    private void Awake() 
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one Music Player! "+ transform + " - " + Instance);
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(this);

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

    public void PlayLevel1Music ()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.clip = level1Clip;
        audioSource.Play();
    }
    public void PlayLevel2Music ()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.clip = level2Clip;
        audioSource.Play();
    }
}
