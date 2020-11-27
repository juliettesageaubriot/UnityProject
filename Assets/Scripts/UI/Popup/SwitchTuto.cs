using System;
using System.Collections;
using Global;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Popup
{
    public class SwitchTuto : AbstractPopup
    {
        [SerializeField] private UIFade fade;
        [SerializeField] private InputData switchInput;
        
        public override void PopIn()
        {
            fade.FadeIn();
        }

        public override void PopOut()
        {
            fade.FadeOut();
            StartCoroutine(WaitForDestroy());
        }
        
        private void OnEnable()
        { switchInput.InputEvent += HandleSwitch; }
        
        private void OnDisable()
        { switchInput.InputEvent -= HandleSwitch; }

        private void HandleSwitch(InputAction.CallbackContext context)
        {
            PopOut();
        }

        private IEnumerator WaitForDestroy()
        {
            yield return new WaitForSeconds(fade.FadeDuration);
            DestroyPopup();
        }
    }
}
