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
        [SerializeField] private GameObject firstButtonUI;
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private ScriptableSceneManager sceneManager;
        [SerializeField] private InputData pauseInput;
        [SerializeField] private InputData[] inputsToDisable;


        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
            foreach (var input in inputsToDisable) input.Enable();
            
            if(data.State == SensesState.Blind) SnapshotManager.Instance.UnmuffleSound(0f);
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
            eventSystem.SetSelectedGameObject(firstButtonUI);
            foreach (var input in inputsToDisable) input.Disable();

            if (data.State == SensesState.Deaf) return;
            SnapshotManager.Instance.PauseSound(0f);
        }

        private void OnEnable()
        {
            sceneManager.BeforeSceneChangeEvent += Resume;
            pauseInput.InputEvent += RegisterInput;
        }

        private void OnDisable()
        {
            sceneManager.BeforeSceneChangeEvent -= Resume;
            pauseInput.InputEvent -= RegisterInput;
        }
        
        private void Start()
        {
            pauseMenuUI.SetActive(false);
            eventSystem.SetSelectedGameObject(firstButtonUI);
        }

        private void RegisterInput(InputAction.CallbackContext callbackContext)
        {
            if (gameIsPaused) Resume();
            else Pause();
        }
    }
}
