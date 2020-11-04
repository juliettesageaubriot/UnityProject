using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelsEnum nextLevel;

        public void OnNextLevel()
        {
            var level = ScenesEnum.LevelsNames(nextLevel); 
            SceneManager.LoadScene(level);
        }
    }
}
