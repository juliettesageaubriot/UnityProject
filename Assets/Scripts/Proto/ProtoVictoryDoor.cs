using Global;
using Player;
using UnityEngine;
using Utils;

namespace Proto
{
    public class ProtoVictoryDoor : MonoBehaviour
    {
        [SerializeField] private PlayerInputData playerInputData;
        [SerializeField] private SingleUnityLayer playerLayer;
        [SerializeField] private Vector2[] directions;
            
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == playerLayer.LayerIndex) EndAnim(other.gameObject);
            
        }

        private void EndAnim(GameObject player)
        {
            var controller = player.GetComponent<PlayerController>();
            playerInputData.DisableAll();
            foreach (var direction in directions)
                controller.AddMoveToQueue(direction);
        }
    }
}