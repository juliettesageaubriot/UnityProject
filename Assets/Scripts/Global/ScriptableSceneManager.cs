using System;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    
    [CreateAssetMenu(fileName = "SceneManager", menuName = "ScriptableObjects/ScriptableSceneManager", order = 1)]
    public class ScriptableSceneManager : ScriptableObject
    {
        [SerializeField] private string[] gameSceneQueueList;
        [SerializeField] private string mainMenuSceneName;
        [SerializeField] private string endSceneName;
        [SerializeField] private ScriptableResetCounter scriptableResetCounter;
        
        public delegate void BeforeSceneChangeHandler();
        public event BeforeSceneChangeHandler BeforeSceneChangeEvent;

        public void StartGame()
        {
            LoadScene(gameSceneQueueList[0]);
        }

        public void GoToMainMenu()
        {
            LoadScene(mainMenuSceneName);
        }

        public void LoadNextLevel()
        {
            var currentScene = SceneManager.GetActiveScene();
            
            var currentLevelIndex = Array.FindIndex(gameSceneQueueList, sceneName => currentScene.name == sceneName);
            if (currentLevelIndex == -1) throw new Exception("Cannot go to next level, the current scene isn't a level.");
            
            LoadScene(currentLevelIndex < gameSceneQueueList.Length - 1
                ? gameSceneQueueList[currentLevelIndex + 1]
                : endSceneName);
        }

        public void ResetCurrentLevel()
        {
            var currentScene = SceneManager.GetActiveScene();
            scriptableResetCounter.OnReset();
            LoadScene(currentScene.name);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
        
        public void OpenUrlForm()
        {
            Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSfSN4LFlsQJ6qcnRbU9bSnOHxUTkuJ62vxh_UiWcksAsoAWHw/viewform?usp=sf_link");
        }

        private void LoadScene(string sceneName)
        {
            BeforeSceneChangeEvent?.Invoke();
            SceneManager.LoadScene(sceneName);
        }
    }
}