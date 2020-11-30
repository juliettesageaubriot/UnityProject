using System;
using UnityEngine;

namespace UI.Popup
{
    public enum DisappearBeforeNext
    {
        Always,
        DifferentName,
        Never
    }
    
    public enum Skip
    {
        DifferentName,
        SameName,
        Never
    }
    
    [Serializable]
    public struct PopupParameters
    {
        public string popupName;
        public GameObject popup;
        public bool autoRemove;
        public float autoRemoveDelay;
        public DisappearBeforeNext disappearBeforeNext;
        public Skip skipIf;
        public bool waitForPreviousDisappear;
        public bool destroyImmediatePrevious;
    }
}