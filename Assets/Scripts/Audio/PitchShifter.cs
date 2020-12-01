using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class PitchShifter : MonoBehaviour
    {
        [SerializeField] private AudioClip audio;
        [SerializeField][Range(0, 0.5f)] private float pitchVariance;
        [SerializeField] private bool shiftAtStart;
        private AudioSource _audioSource;
        private float _defaultPitch;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _defaultPitch = _audioSource.pitch;
            if (shiftAtStart) ShiftPitch();
        }

        public void Play(bool shouldShiftPitch)
        {
            Play(shouldShiftPitch, audio);
        }
        public void Play(AudioClip audioClip)
        {
            Play(true, audioClip);
        }
        public void Play(bool shouldShiftPitch, AudioClip audioClip)
        {
            if (shouldShiftPitch) ShiftPitch();
            _audioSource.clip = audioClip;
            _audioSource.time = 0f;
            _audioSource.Play();
        }

        public void ShiftPitch()
        {
            _audioSource.pitch = _defaultPitch + Random.Range(-pitchVariance, pitchVariance);
            // Example
            // For pitchVariance = 0.05 and defaultPitch = 1
            // outPitch = 1 more or less 0.05 ( from 0.95 to 1.05 )
        }
    }
}
