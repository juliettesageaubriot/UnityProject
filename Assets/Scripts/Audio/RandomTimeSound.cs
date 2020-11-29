using UnityEngine;

namespace Audio
{
    public class RandomTimeSound : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private bool startAtAwake;
    
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = audioClip;
            if (startAtAwake) _audioSource.Play();
        }

        private void Play()
        {
            var randomTime = Random.value * audioClip.length;
            _audioSource.time = randomTime;
            _audioSource.Play();
        }
    }
}
