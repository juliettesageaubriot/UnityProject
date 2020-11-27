using Player;
using UnityEngine;

namespace UI
{
    public class InteractButton : InputButton
    {
        [SerializeField] private PlayerInteractData interactData;

        private bool _canInteract;

        protected override void OnEnable()
        {
            interactData.CanInteractChange += UpdateButtonInteractable;
            base.OnEnable();
        }

        protected override void OnDisable()
        { 
            interactData.CanInteractChange -= UpdateButtonInteractable;
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
            if (isNowInteractable != button.interactable)
            {
                if (isNowInteractable) fade.FadeIn(duration);
                else fade.FadeOut(duration);
            }
            button.interactable = isNowInteractable;
        }
    }
}