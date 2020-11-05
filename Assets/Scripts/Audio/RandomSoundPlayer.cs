using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    public class RandomSoundPlayer : MonoBehaviour
    {
        [SerializeField] protected SoundCollection sounds;
        private AudioSource _audioSource;

        protected virtual void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayRandomSound()
        {
            _audioSource.PlayOneShot(sounds.GetRandomAudio());
        }

        public void PlayRandomSound(Action callback)
        {
            var clip = sounds.GetRandomAudio();
            _audioSource.PlayOneShot(clip);
            StartCoroutine(WaitForClipToEnd(clip.length, callback));
        }

        private static IEnumerator WaitForClipToEnd(float clipLength, Action callback)
        {
            yield return new WaitForSeconds(clipLength);
            callback();
        }
    }
}
