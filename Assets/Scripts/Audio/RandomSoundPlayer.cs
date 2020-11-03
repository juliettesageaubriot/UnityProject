using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSoundPlayer : MonoBehaviour
{
    [SerializeField] private SoundData sounds;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSound()
    {
        var randomIndex = Mathf.FloorToInt(Random.Range(0f, sounds.array.Length));
        _audioSource.PlayOneShot(sounds.array[randomIndex]);
    }
}
