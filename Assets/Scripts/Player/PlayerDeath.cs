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
        [SerializeField] private PlayerInputData playerInput;

        private void OnEnable()
        {
            if (InputManager.IsReady) InputManager.ActionMaps.Player.Reset.performed += HandleResetInput;
        }

        private void OnDisable()
        {
            if (InputManager.IsReady) InputManager.ActionMaps.Player.Reset.performed -= HandleResetInput;
        }
        
        private void HandleResetInput(InputAction.CallbackContext context)
        {
            if (!playerInput.Can.reset) return;
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

