using Global;
using Player;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Interactables
{
    public class PlayerKiller : MonoBehaviour
    {
        [SerializeField] private SingleUnityLayer playerLayer;
        [SerializeField] private PlayerInputData inputData;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != playerLayer.LayerIndex) return;
            
            OnKill();
            inputData.DisableAll();
            other.GetComponent<PlayerController>().enabled = false;
            other.GetComponent<PlayerDeath>().Kill();
        }

        protected virtual void OnKill()
        { }
    }
}
