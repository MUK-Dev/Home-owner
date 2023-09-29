using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager Instance { get; private set; }

    private AudioSource _soundSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        _soundSource = GetComponent<AudioSource>();
    }

    public void PlayBeepSound()
    {
        _soundSource.Play();
    }
}
