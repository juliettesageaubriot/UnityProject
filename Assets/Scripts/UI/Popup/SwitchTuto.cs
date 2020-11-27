using System;
using System.Collections;
using Global;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Popup
{
    public class SwitchTuto : FadePopup
    {
        [SerializeField] private InputData switchInput;
        private bool _listenInputs;

        private void OnEnable()
        {
            switchInput.InputEvent += HandleSwitch;
            _listenInputs = true;
        }
        
        private void OnDisable()
        {
            if (_listenInputs) switchInput.InputEvent -= HandleSwitch;
            _listenInputs = false;
        }

        private void HandleSwitch(InputAction.CallbackContext context)
        {
            PopOut();
            if (_listenInputs) switchInput.InputEvent -= HandleSwitch;
            _listenInputs = false;
        }
    }
}
