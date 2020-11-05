using Global.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

        public void Resume()
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
        
        public void MainMenu ()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Scenes/Proto/Main Menu");
        }
        
        public void Quit ()
        {
            Application.Quit();
        }
    }
}
