using System;
using Global;
using Global.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool gameIsPaused = false;

        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private ScriptableSceneManager sceneManager;
        
        private void OnEnable()
        {
            sceneManager.BeforeSceneChangeEvent += Resume;
            if(InputManager.IsReady) InputManager.ActionMaps.Player.Pause.performed += RegisterInput;
        }

        private void OnDisable()
        {
            sceneManager.BeforeSceneChangeEvent -= Resume;
            if(InputManager.IsReady) InputManager.ActionMaps.Player.Pause.performed -= RegisterInput;
        }

        private void Start()
        {
            pauseMenuUI.SetActive(false);
        }

        private void RegisterInput(InputAction.CallbackContext context)
        {
            if (gameIsPaused) Resume();
            else Pause();
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }
    }
}
