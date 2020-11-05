using System;
using Global.Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerReset : MonoBehaviour
    {
        [SerializeField]
        public class MoveEvent : UnityEvent {}

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
            Reset();
        }

        public void Reset()
        {
            var activeScene = SceneManager.GetActiveScene();
            var sceneActiveName = activeScene.name;
            SceneManager.LoadScene(sceneActiveName);
        }
    }
}

