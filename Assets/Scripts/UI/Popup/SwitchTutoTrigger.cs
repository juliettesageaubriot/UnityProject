using System;
using Global;
using UnityEngine;

namespace UI.Popup
{
    public class SwitchTutoTrigger : PopupSender
    {
        [SerializeField] private string fuelTutoPopupName;
        [SerializeField] private UIFade buttonFade;
        [SerializeField] private UIFade fuelIndicatorFade;
        
        private void OnEnable()
        {
            bus.DestroyPopupEvent += OnPopupClose;
        }
        private void OnDisable()
        {
            bus.DestroyPopupEvent -= OnPopupClose;
        }

        private void OnPopupClose(PopupParameters popup)
        {
            if (popup.popupName != fuelTutoPopupName) return;
            buttonFade.FadeIn();
            fuelIndicatorFade.FadeIn();
            SendPopup();
        }
    }
}
