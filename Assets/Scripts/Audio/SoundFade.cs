using System;
using DG.Tweening;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundFade : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] private float inVolume = 1f;
        [SerializeField] [Range(0, 1)] private float outVolume = 0f;
        [SerializeField] private float fadeDuration = 0f;
        [SerializeField] private bool startIn = true;
        [SerializeField] private bool useSourceVolumeAsInVolume = false;

        private AudioSource _audioSource;
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            if (useSourceVolumeAsInVolume) inVolume = _audioSource.volume;
            if (startIn)
                FadeIn(0f);
            else
                FadeOut(0f);
        }

        public void FadeIn()
        {
            FadeIn(fadeDuration);
        }
        public void FadeIn(float duration)
        {
            _audioSource.DOFade(inVolume, duration);
        }

        public void FadeOut()
        {
            FadeOut(fadeDuration);
        }
        public void FadeOut(float duration)
        {
            _audioSource.DOFade(outVolume, duration);
        }
    }
}
