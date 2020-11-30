using System;
using Player;
using UnityEngine;

namespace UI.Popup
{
    [DefaultExecutionOrder(-100)]
    public class FirstBlindPopupTrigger : PopupSender
    {
        [SerializeField] private string cinematicPopupName;
        private bool _listenInputs;
        
        private void OnEnable()
        {
            _listenInputs = true;
            bus.DestroyPopupEvent += HandleSenseChange;
        }
        
        private void OnDisable()
        {
            _listenInputs = false;
            if (_listenInputs) bus.DestroyPopupEvent -= HandleSenseChange;
        }

        private void HandleSenseChange(PopupParameters popup)
        {
            if (!_listenInputs) return;
            if (popup.popupName != cinematicPopupName) return;
            if (_listenInputs) bus.DestroyPopupEvent -= HandleSenseChange;
            _listenInputs = false;
            SendPopup();
        }
    }
}
