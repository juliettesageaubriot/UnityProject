using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame ()
        {
            SceneManager.LoadScene("Scenes/POC");
        }
    }
}
