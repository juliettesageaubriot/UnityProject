using UnityEngine;
using Utils;

namespace UI.Popup
{
    [RequireComponent(typeof(EventSequence))]
    public class PopupSequence : AbstractPopup
    {
        [SerializeField] private EventSequence popInSequence;
        [SerializeField] private EventSequence popOutSequence;
        
        public override void PopIn()
        {
            if (popInSequence != null) popInSequence.StartSequence();
        }

        public override void PopOut()
        {
            if (IsPopingOut) return;
            if (popOutSequence != null) popOutSequence.StartSequence();
            base.PopOut();
        }
        
        public new void DestroyPopup()
        {
            base.DestroyPopup();
        }
    }
}