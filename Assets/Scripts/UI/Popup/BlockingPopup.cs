using System.Collections;
using Global;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace UI.Popup
{
    public class BlockingPopup : PopupSequence
    {
        [SerializeField] private PlayerInputData playerInputData;
        [SerializeField] private InputData interactInput;
        [SerializeField] private MultipleTexts multipleTexts;
        
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
            if (multipleTexts.IsAnimating) multipleTexts.CompleteCurrentText();
            else
            {
                if (multipleTexts.IsLastText())
                    PopOut();
                else
                    multipleTexts.PlayNextText();
            }
                
        }
        
        public new void DestroyPopup()
        {
            base.DestroyPopup();
        }
    }
}