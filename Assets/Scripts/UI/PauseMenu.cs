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


        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
            
            if(data.State == SensesState.Blind) SnapshotManager.Instance.UnmuffleSound(0f);
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
            eventSystem.SetSelectedGameObject(firstButtonUI);

            if (data.State == SensesState.Deaf) return;
            SnapshotManager.Instance.PauseSound(0f);
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
            eventSystem.SetSelectedGameObject(firstButtonUI);
        }

        private void RegisterInput(InputAction.CallbackContext callbackContext)
        {
            if (gameIsPaused) Resume();
            else Pause();
        }
    }
}
