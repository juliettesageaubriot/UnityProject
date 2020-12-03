using System;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Popup
{
    public class BlockingPopupDialogue : PopupSender
    {
        [SerializeField] private GameObject[] popups;
        [SerializeField] private UnityEvent endEvent;
        private int _index;

        private void Start()
        {
            if (endEvent == null)
                endEvent = new UnityEvent();
        }

        private void OnEnable()
        {
            bus.DestroyPopupEvent += HandlePopupEvent;
        }
        private void OnDisable()
        {
            bus.DestroyPopupEvent -= HandlePopupEvent;
        }

        private void HandlePopupEvent(PopupParameters popupParams)
        {
            if (popupParams.popupName == popupParameters.popupName)
                NextDialogue();
        }

        public void StartDialogue()
        {
            popupParameters.popup = popups[_index];
            SendPopup();
        }

        private void NextDialogue()
        {
            _index++;
            if (_index == popups.Length)
            {
                endEvent.Invoke();
                return;
            }
            popupParameters.popup = popups[_index];
            SendPopup();
        }
    }
}
