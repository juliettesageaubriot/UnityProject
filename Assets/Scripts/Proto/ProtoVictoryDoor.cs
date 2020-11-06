using Global;
using UnityEngine;

namespace Proto
{
    public class ProtoVictoryDoor : MonoBehaviour
    {
        [SerializeField] private ScriptableSceneManager sceneManager;
            
        private void OnTriggerEnter2D(Collider2D other)
        {
            sceneManager.LoadNextLevel();
        }
    }
}