using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    
    [CreateAssetMenu(fileName = "SceneManager", menuName = "ScriptableObjects/ScriptableSceneManager", order = 1)]
    public class ScriptableSceneManager : ScriptableObject
    {
        [SerializeField] private string[] levelNameList;
        [SerializeField] private string mainMenuSceneName;
        
        public delegate void BeforeSceneChangeHandler();
        public event BeforeSceneChangeHandler BeforeSceneChangeEvent;

        public void StartGame()
        {
            LoadScene(levelNameList[0]);
        }

        public void GoToMainMenu()
        {
            LoadScene(mainMenuSceneName);
        }

        public void LoadNextLevel()
        {
            var currentScene = SceneManager.GetActiveScene();
            
            var currentLevelIndex = Array.FindIndex(levelNameList, sceneName => currentScene.name == sceneName);
            if (currentLevelIndex == -1) throw new Exception("Cannot go to next level, the current scene isn't a level.");
            
            LoadScene(currentLevelIndex < levelNameList.Length - 1
                ? levelNameList[currentLevelIndex + 1]
                : mainMenuSceneName);
        }

        public void ResetCurrentLevel()
        {
            var currentScene = SceneManager.GetActiveScene();
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