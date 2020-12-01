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
        private PlayerInteractData interactData;
        [SerializeField]
        private InputData interactInput;
        [SerializeField] 
        private PlayerPositionData positionData;
        [SerializeField]
        private Vector2 raycastCenter = Vector2.zero;
        [SerializeField]
        private float maxDistance = 1f;
        [SerializeField] 
        private LayerMask interactionLayer;

        private IActionable _actionable = null;
        private bool _lastCanInteract;

        public bool CanInteract => _actionable != null && _actionable.IsActionable();

        private void Start()
        {
            interactData.CanInteractHasChanged(CanInteract);
        }

        private void OnEnable()
        {
            interactInput.InputEvent += Interact;
            interactData.CheckInteractEvent += CheckInteract;
            interactData.InteractEvent += Interact;
        }

        private void OnDisable()
        {
            interactInput.InputEvent -= Interact;
            interactData.CheckInteractEvent -= CheckInteract;
            interactData.InteractEvent -= Interact;
        }

        private void Update()
        {
            var newCanInteract = CanInteract;
            if (_lastCanInteract != newCanInteract)
                interactData.CanInteractHasChanged(newCanInteract);
            _lastCanInteract = CanInteract;
        }

        public void CheckInteract(Vector2 direction)
        {
            var hit = Physics2D.Raycast(
                transform.position + new Vector3(raycastCenter.x, raycastCenter.y, 0f),
                direction,
                maxDistance,
                interactionLayer
            );

            if (hit.collider == null)
            {
                _actionable = null;
                return;
            }

            var actionableComponent = hit.collider.gameObject.GetComponent<IActionable>();
            if (actionableComponent == null)
                throw new NullReferenceException(hit.collider.gameObject.name +
                                                 " doesn't have any actionable script.");
            _actionable = actionableComponent;
        }
        public void CheckInteract()
        {
            CheckInteract(positionData.Direction);
        }

        private void Interact()
        {
            if (CanInteract) _actionable.Action();
        }

        private void Interact(InputAction.CallbackContext callbackContext)
        {
            Interact();
        }
        
    }
}