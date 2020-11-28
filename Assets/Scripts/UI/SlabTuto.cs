using UnityEngine;

namespace UI
{
    public class SlabTuto : PopupSender
    {
        [SerializeField] private ScriptableResetCounter scriptableResetCounter;
        [SerializeField] private int resetAmountBeforeShowing = 4;
        
        private void Start()
        {
            if (scriptableResetCounter.ResetCounter > resetAmountBeforeShowing)
                SendPopup();
        }
    }
}
