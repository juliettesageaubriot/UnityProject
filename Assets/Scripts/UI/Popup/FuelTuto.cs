using System.Collections;
using Global;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Popup
{
    public class FuelTuto : AbstractPopup
    {
        [SerializeField] private UIFade fade;
        [SerializeField] private PlayerInputData playerInputData;
        [SerializeField] private InputData interactInput;
        
        public override void PopIn()
        {
            playerInputData.DisableAll();
            fade.FadeIn();
        }

        public override void PopOut()
        {
            playerInputData.EnableAll();
            fade.FadeOut();
            StartCoroutine(DestroyAfterTime());
        }

        private IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(fade.FadeDuration);
            DestroyPopup();
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
