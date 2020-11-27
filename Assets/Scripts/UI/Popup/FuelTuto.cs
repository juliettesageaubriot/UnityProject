using System.Collections;
using Global;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Popup
{
    public class FuelTuto : FadePopup
    {
        [SerializeField] private PlayerInputData playerInputData;
        [SerializeField] private InputData interactInput;
        
        public override void PopIn()
        {
            playerInputData.DisableAll();
            base.PopIn();
        }

        public override void PopOut()
        {
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
    }
}
