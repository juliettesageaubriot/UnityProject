using System;
using UnityEngine;

namespace UI.Popup
{
    public enum RemovePrevious
    {
        Always,
        Never
    }
    
    [Serializable]
    public struct PopupParameters
    {
        public string popupName;
        public GameObject popup;
        public bool autoRemove;
        public float autoRemoveDelay;
        public RemovePrevious removePrevious;
        public bool waitForRemovePrevious;
    }
}