using System.Collections;
using Global;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace UI.Popup
{
    public class FuelTuto : AbstractPopup
    {
        [SerializeField] private PlayerInputData playerInputData;
        [SerializeField] private InputData interactInput;
        [SerializeField] private EventSequence popOutSequence;
        
        public override void PopIn()
        {
            playerInputData.DisableAll();
        }

        public override void PopOut()
        {
            popOutSequence.StartSequence();
            playerInputData.EnableAll();
            base.PopOut();
        }
        
        private void OnEnable()
        {
            if (interactInput.BaseEvent != null) interactInput.BaseEvent.performed += HandleSkip;
        }
        private void OnDisable()
        {
            if (interactInput.BaseEvent != null) interactInput.BaseEvent.performed -= HandleSkip;
        }

        private void HandleSkip(InputAction.CallbackContext callbackContext)
        {
            PopOut();
        }
        
        public new void DestroyPopup()
        {
            base.DestroyPopup();
        }
    }
}
