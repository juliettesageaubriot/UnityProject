using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelsEnum nextLevel;

        public void OnNextLevel()
        {
            var name = ScenesEnum.LevelsNames(nextLevel);
            var test = SceneManager.GetSceneByName(name);
            
            Debug.Log(test);
        }
    }
}
