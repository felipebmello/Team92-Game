using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioListener audioListener;
    public static AudioManager Instance { get; private set; }


    private void Awake() 
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one LevelSystem! "+ transform + " - " + Instance);
            Destroy(gameObject);
        }
        Instance = this;
        audioListener = GetComponent<AudioListener>();
    }

    public AudioListener GetAudioListener()
    {
        return audioListener;
    }



}
