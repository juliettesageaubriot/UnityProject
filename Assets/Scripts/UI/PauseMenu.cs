using Audio;
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
        
        [SerializeField] private PlayerSensesData data;
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject mainTabUI;
        [SerializeField] private GameObject optionTabUI;
        [SerializeField] private GameObject firstMainButtonUI;
        [SerializeField] private GameObject firstOptionButtonUI;
        [SerializeField] private EventSystem eventSystem;
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
            CloseOptionTab();
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
            
            if(data.State == SensesState.Blind) SnapshotManager.Instance.UnmuffleSound();
        }

        public void Pause()
        {
            
            if (!_canPause) return;
            playerInputData.SetMoveEnable(false);
            playerInputData.SetSwitchEnable(false);
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;

            if (data.State == SensesState.Deaf) return;
            SnapshotManager.Instance.PauseSound();
        }

        public void OpenOptionTab()
        {
            optionTabUI.SetActive(true);
            mainTabUI.SetActive(false);
            eventSystem.SetSelectedGameObject(firstOptionButtonUI);
        }
        
        public void CloseOptionTab()
        {
            optionTabUI.SetActive(false);
            mainTabUI.SetActive(true);
            eventSystem.SetSelectedGameObject(firstMainButtonUI);
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
            eventSystem.SetSelectedGameObject(firstMainButtonUI);
        }

        private void RegisterInput(InputAction.CallbackContext context)
        {
            if (gameIsPaused) Resume();
            else Pause();
        }
    }
}
