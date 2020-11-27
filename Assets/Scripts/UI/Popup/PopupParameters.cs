using System;
using UnityEngine;

namespace UI.Popup
{
    [Serializable]
    public struct PopupParameters
    {
        public string popupName;
        public GameObject popup;
        public float autoRemoveDelay;
        public bool removePrevious;
        public bool autoRemove;
    }
}