using System.Collections;
using DG.Tweening;
using Global;
using Graphics;
using Player;
using UnityEngine;
using Utils;

namespace Proto
{
    public class ProtoVictoryDoor : MonoBehaviour
    {
        [SerializeField] private PlayerInputData playerInputData;
        [SerializeField] private ScriptableSceneManager sceneManager;
        [SerializeField] private float nextSceneDelay = 1.5f;
        [SerializeField] private SingleUnityLayer playerLayer;
        [SerializeField] private ScriptableCameraAnimator cameraAnimator;
        [SerializeField] private Vector2[] directions;
            
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == playerLayer.LayerIndex) EndAnim(other.gameObject);
            
        }

        private void EndAnim(GameObject player)
        {
            cameraAnimator.TravelingOut();
            var controller = player.GetComponent<PlayerController>();
            playerInputData.DisableAll();
            foreach (var direction in directions)
                controller.AddMoveToQueue(direction);
            player.transform.Find("PlayerSprite").GetComponent<SpriteRenderer>().DOColor(Color.black, 0.4f);

            StartCoroutine(WaitBeforeNextScene());
        }

        private IEnumerator WaitBeforeNextScene()
        {
            yield return new WaitForSeconds(nextSceneDelay);
            sceneManager.LoadNextLevel();
        }
    }
}