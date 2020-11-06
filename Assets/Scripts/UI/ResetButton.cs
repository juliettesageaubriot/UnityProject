using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class ResetButton : MonoBehaviour
    {
        public  void Reset()
        {
            var activeScene = SceneManager.GetActiveScene();
            var sceneActiveName = activeScene.name;
            SceneManager.LoadScene(sceneActiveName);
        }
    }
}