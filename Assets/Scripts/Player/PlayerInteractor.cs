using System;
using Global;
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
        private InputData interactInput;
        [SerializeField] 
        private PlayerPositionData positionData;
        [SerializeField]
        private Vector2 raycastCenter = Vector2.zero;
        [SerializeField]
        private float maxDistance = 1f;
        [SerializeField] 
        private SingleUnityLayer interactionLayer;

        private void OnEnable()
        { interactInput.InputEvent += Interact; }

        private void OnDisable()
        { interactInput.InputEvent -= Interact; }

        private void Interact(InputAction.CallbackContext callbackContext)
        {
            
            var hit = Physics2D.Raycast(
                transform.position + new Vector3(raycastCenter.x, raycastCenter.y, 0f),
                positionData.Direction,
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
        
    }
}