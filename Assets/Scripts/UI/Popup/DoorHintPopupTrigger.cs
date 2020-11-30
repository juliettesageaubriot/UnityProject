using System;
using Interactables;
using UnityEngine;

namespace UI.Popup
{
    public class DoorHintPopupTrigger : ActionPopupTrigger
    {
        [SerializeField] private GameObject openPopup;
        [SerializeField] private GameObject closedPopup;

        public void HandleInitDoor(bool isOpen)
        {
            if (isOpen)
                SetOpenPopup();
            else
                SetClosePopup();
        }
        
        public void SetClosePopup()
        {
            popupParameters.popup = closedPopup;
        }
        public void SetOpenPopup()
        {
            popupParameters.popup = openPopup;
        }
    }
}
