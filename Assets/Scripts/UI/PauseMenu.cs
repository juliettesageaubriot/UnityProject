using Global;
using Global.Input;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool gameIsPaused = false;

        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject mainTabUI;
        [SerializeField] private GameObject optionTabUI;
        [SerializeField] private GameObject fisrtMainButtonUI;
        [SerializeField] private GameObject fisrtOptionButtonUI;
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private ScriptableSceneManager sceneManager;
        [SerializeField] private InputData moveInput;
        [SerializeField] private InputData switchInput;
        [SerializeField] private InputData pauseInput;


        public void Resume()
        {
            moveInput.Enable();
            switchInput.Enable();
            CloseOptionTab();
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }

        public void Pause()
        {
            moveInput.Disable();
            switchInput.Disable();
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }
        
        public void OpenOptionTab()
        {
            optionTabUI.SetActive(true);
            mainTabUI.SetActive(false);
            eventSystem.SetSelectedGameObject(fisrtOptionButtonUI);
        }
        
        public void CloseOptionTab()
        {
            optionTabUI.SetActive(false);
            mainTabUI.SetActive(true);
            eventSystem.SetSelectedGameObject(fisrtMainButtonUI);
        }

        private void OnEnable()
        {
            sceneManager.BeforeSceneChangeEvent += Resume;
            pauseInput.AddListener(RegisterInput);
        }

        private void OnDisable()
        {
            sceneManager.BeforeSceneChangeEvent -= Resume;
            pauseInput.RemoveListener(RegisterInput);
        }
        
        private void Start()
        {
            pauseMenuUI.SetActive(false);
            eventSystem.SetSelectedGameObject(fisrtMainButtonUI);
        }

        private void RegisterInput(InputAction.CallbackContext callbackContext)
        {
            if (gameIsPaused) Resume();
            else Pause();
        }
    }
}
