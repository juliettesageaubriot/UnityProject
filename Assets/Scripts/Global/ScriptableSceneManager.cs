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
            Debug.Log("Start Game");
            LoadScene(levelNameList[0]);
            SceneManager.LoadScene(levelNameList[0]);
        }

        public void GoToMainMenu()
        {
            LoadScene(mainMenuSceneName);
        }

        public void LoadNextLevel()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            
            var currentLevelIndex = Array.FindIndex(levelNameList, sceneName => currentScene.name == sceneName);
            if (currentLevelIndex == -1) throw new Exception("Cannot go to next level, the current scene isn't a level.");
            
            var nextLevelName = levelNameList[(currentLevelIndex + 1) % levelNameList.Length];
            
            LoadScene(nextLevelName);
        }

        public void ResetCurrentLevel()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            LoadScene(currentScene.name);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void LoadScene(string sceneName)
        {
            BeforeSceneChangeEvent?.Invoke();
            SceneManager.LoadScene(sceneName);
        }
    }
}