using System.Collections;
using Global;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
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
            if (popOutSequence != null) popOutSequence.StartSequence();
        }
        
        public new void DestroyPopup()
        {
            base.DestroyPopup();
        }
    }
}