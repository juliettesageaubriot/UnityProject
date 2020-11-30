using Player;
using UnityEngine;

namespace UI
{
    public class InteractButton : InputButton
    {
        [SerializeField] private PlayerInteractData interactData;

        private bool _canInteract;
        private bool _isFirstUpdate = true;
        
        protected override void OnEnable()
        {
            interactData.CanInteractChange += HandleCanInteractChange;
            base.OnEnable();
        }

        protected override void OnDisable()
        { 
            interactData.CanInteractChange -= HandleCanInteractChange;
            base.OnDisable();
        }

        private void HandleCanInteractChange(bool canInteract)
        {
            _canInteract = canInteract;
            UpdateButtonInteractable(_canInteract);
        }

        protected override void UpdateButtonInteractable(bool isInputEnable, float duration)
        {
            UpdateButtonInteractable(_canInteract, isInputEnable, duration);
        }

        private void UpdateButtonInteractable(bool canInteract)
        {
            UpdateButtonInteractable(canInteract, inputData.Can, fade.FadeDuration);
        }

        private void UpdateButtonInteractable(bool canInteract, bool isInputEnable, float duration)
        {
            var isNowInteractable = canInteract && isInputEnable;
            if (isNowInteractable != button.interactable || _isFirstUpdate)
            {
                if (isNowInteractable) fade.FadeIn(_isFirstUpdate ? 0f : duration);
                else fade.FadeOut(_isFirstUpdate ? 0f : duration);
                _isFirstUpdate = false;
            }
            button.interactable = isNowInteractable;
        }
    }
}