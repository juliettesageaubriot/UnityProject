using System;
using Global;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Audio
{
    public class ResetSoundPlayer : MonoBehaviour
    {
        [SerializeField] private InputData resetInput;
        [SerializeField] private AudioSource audioSource;

        private void OnEnable()
        {
            resetInput.InputEvent += PlaySound;
        }
        
        private void OnDisable()
        {
            resetInput.InputEvent -= PlaySound;
        }

        private void PlaySound(InputAction.CallbackContext context)
        {
            audioSource.Play();
        }
    }
}
