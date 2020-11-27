using System;
using System.Collections;
using Global;
using Global.Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerDeath : MonoBehaviour
    {

        [SerializeField] private ScriptableSceneManager sceneManager;
        [SerializeField] private InputData resetInput;

        private void OnEnable()
        {
            resetInput.InputEvent += HandleResetInput;
        }

        private void OnDisable()
        {
            resetInput.InputEvent -= HandleResetInput;
        }
        
        private void HandleResetInput(InputAction.CallbackContext context)
        {
            sceneManager.ResetCurrentLevel();
        }

        public void Kill()
        {
            StartCoroutine(WaitBeforeReset());
        }

        private IEnumerator WaitBeforeReset()
        {
            yield return new WaitForSeconds(1);
            sceneManager.ResetCurrentLevel();
        }
    }
}

