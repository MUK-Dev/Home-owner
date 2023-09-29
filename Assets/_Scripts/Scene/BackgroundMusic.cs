using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip[] _audioClips;

    private int _previousSongIndex;

    private void Start()
    {
        //! So all screens will have the same music playing
        DontDestroyOnLoad(this);

        _musicSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!_musicSource.isPlaying)
        {
            _musicSource.clip = GetRandomAudioClip();

            _musicSource.Play();
        }
    }

    private AudioClip GetRandomAudioClip()
    {
        //! Generate a random index for the music array
        int generatedRandomIndex = Random.Range(0, _audioClips.Length);

        //! if the generated index is same as the last song playing index
        //! then create a new one
        while (_previousSongIndex == generatedRandomIndex)
        {
            generatedRandomIndex = Random.Range(0, _audioClips.Length);
        }

        //! Set the previous song index to the new generated index
        _previousSongIndex = generatedRandomIndex;

        return _audioClips[generatedRandomIndex];
    }

}
