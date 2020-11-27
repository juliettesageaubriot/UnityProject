using System;
using UnityEngine;

namespace UI.Popup
{
    [Serializable]
    public struct PopupParameters
    {
        public string popupName;
        public GameObject popup;
        public bool autoRemove;
        public float autoRemoveDelay;
        public bool removePrevious;
    }
}