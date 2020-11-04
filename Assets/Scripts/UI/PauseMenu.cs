using Global.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool gameIsPaused = false;

        public GameObject pauseMenuUI;
        
        private void OnEnable()
        {
            if(InputManager.IsReady) InputManager.ActionMaps.Player.Resume.performed += RegisterInput;
        }

        private void OnDisable()
        {
            if(InputManager.IsReady) InputManager.ActionMaps.Player.Resume.performed -= RegisterInput;
        }
        private void RegisterInput(InputAction.CallbackContext context)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }

        void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }
    }
}
