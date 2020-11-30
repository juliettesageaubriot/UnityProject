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
        [SerializeField] private MultipleTexts multipleTexts;
        
        public override void PopIn()
        {
            playerInputData.DisableAll();
            multipleTexts.PlayNextText();
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
