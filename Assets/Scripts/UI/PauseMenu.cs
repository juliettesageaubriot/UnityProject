using System;
using Global;
using Global.Input;
using Player;
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
        [SerializeField] private PlayerInputData playerInputData;

        private bool _canPause = true;

        public void EnablePause(bool val)
        { _canPause = val; }

        public void Resume()
        {
            if (!_canPause) return;
            playerInputData.SetMoveEnable(true);
            playerInputData.SetSwitchEnable(true);
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }

        public void Pause()
        {
            if (!_canPause) return;
            playerInputData.SetMoveEnable(false);
            playerInputData.SetSwitchEnable(false);
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }

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
    }
}
