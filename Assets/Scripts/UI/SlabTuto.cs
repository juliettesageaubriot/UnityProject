using UI.Popup;
using UnityEngine;

namespace UI
{
    public class SlabTuto : PopupSender
    {
        [SerializeField] private PopupParameters firstPopup;
        [SerializeField] private ScriptableResetCounter scriptableResetCounter;
        [SerializeField] private int resetAmountBeforeShowing = 4;
        
        private void Start()
        {
            if (scriptableResetCounter.ResetCounter == resetAmountBeforeShowing)
                SendPopup(firstPopup);/*
            if (scriptableResetCounter.ResetCounter > resetAmountBeforeShowing + 1)
                SendPopup();*/
        }
    }
}
