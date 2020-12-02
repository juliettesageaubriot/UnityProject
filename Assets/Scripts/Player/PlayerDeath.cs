using System;
using System.Collections;
using System.Data;
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

        [SerializeField] private float waitBeforeReset = 2f;
        [SerializeField] private ScriptableSceneManager sceneManager;
        [SerializeField] private InputData resetInput;
        [SerializeField] private UnityEvent onDeathEvent;

        private void Start()
        {
            if (onDeathEvent == null)
                onDeathEvent = new UnityEvent();
        }

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
            throw new SyntaxErrorException("test");
            StartCoroutine(WaitBeforeReset());
            onDeathEvent.Invoke();
        }

        private IEnumerator WaitBeforeReset()
        {
            yield return new WaitForSeconds(waitBeforeReset);
            sceneManager.ResetCurrentLevel();
        }
    }
}

