using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class SetVolume : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;

        public void SetLevel(float sliderValue)
        {
            mixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
        }

    }
}