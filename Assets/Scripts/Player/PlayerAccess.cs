using UnityEngine;

namespace Player
{
    public class PlayerAccess : MonoBehaviour
    {
        static public GameObject currentPlayer;

        private void Awake()
        {
            currentPlayer = gameObject;
        }

        private void OnDestroy()
        {
            currentPlayer = null;
        }
    }
}
