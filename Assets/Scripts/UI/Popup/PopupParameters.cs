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
    
    [Serializable]
    public struct PopupParameters
    {
        public string popupName;
        public GameObject popup;
        public bool autoRemove;
        public float autoRemoveDelay;
        public DisappearBeforeNext disappearBeforeNext;
        public bool waitForPreviousDisappear;
    }
}