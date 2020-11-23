using System;
using Global.Input;
using Interactables;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        
        [SerializeField]
        private Vector2 raycastCenter = Vector2.zero;
        [SerializeField]
        private float maxDistance = 1f;
        [SerializeField] 
        private SingleUnityLayer interactionLayer;

        private Vector2 _direction;
        
        private void OnEnable()
        {
            if(InputManager.IsReady) InputManager.ActionMaps.Player.Interact.performed += Interact;
        }

        private void OnDisable()
        {
            if(InputManager.IsReady) InputManager.ActionMaps.Player.Interact.performed -= Interact;
        }

        private void Interact(InputAction.CallbackContext callbackContext)
        {
            
            var hit = Physics2D.Raycast(
                transform.position + new Vector3(raycastCenter.x, raycastCenter.y, 0f),
                _direction,
                maxDistance,
                interactionLayer.Mask
            );
            
            if (hit.collider != null)
            {
                var actionable = hit.collider.gameObject.GetComponent<IActionable>();
                if(actionable == null) throw new NullReferenceException(hit.collider.gameObject.name + " doesn't have any actionable script.");
                actionable.Action();
            }
        }

        public void UpdateDirection(Vector2 newDirection)
        {
            _direction = newDirection;
        }
        
    }
}