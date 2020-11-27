using System;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(UIFade))]
    [RequireComponent(typeof(Button))]
    public class InputButton : MonoBehaviour
    {
        [SerializeField] protected InputData inputData;
        [SerializeField] protected bool forceDisable;

        public bool ForceDisable
        {
            get => forceDisable;
            set
            {
                forceDisable = value;
                UpdateButtonInteractable();
            }
        }
        protected Button button;
        protected UIFade fade;

        private void Awake()
        {
            button = GetComponent<Button>();
            fade = GetComponent<UIFade>();
        }

        private void Start()
        {
            UpdateButtonInteractable(inputData.Can, 0f);
        }

        protected virtual void OnEnable()
        {
            inputData.ChangeEnableEvent += UpdateButtonInteractable;
        }
        protected virtual void OnDisable()
        { 
            inputData.ChangeEnableEvent -= UpdateButtonInteractable;
        }

        private void UpdateButtonInteractable(bool isInputEnable)
        {
            UpdateButtonInteractable(isInputEnable, fade.FadeDuration);
        }
        private void UpdateButtonInteractable()
        {
            UpdateButtonInteractable(inputData.Can, fade.FadeDuration);
        }
        protected virtual void UpdateButtonInteractable(bool isInputEnable, float duration)
        {
            var isNowInteractable = !forceDisable && isInputEnable;
            if (isNowInteractable != button.interactable)
            {
                if (isNowInteractable) fade.FadeIn(duration);
                else fade.FadeOut(duration);
            }
            button.interactable = isNowInteractable;
        }
    }
}