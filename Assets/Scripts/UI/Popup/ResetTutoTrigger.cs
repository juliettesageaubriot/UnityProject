using System;
using System.Collections;
using Player;
using UnityEngine;

namespace UI.Popup
{
    public class ResetTutoTrigger : PopupSender
    {
        [SerializeField] private PlayerSensesData sensesData;
        [SerializeField] private float timeBeforeHint = 8f;
        
        private InputButton _resetButton;
        private bool _listenInputs;

        private void Start()
        {
            _resetButton = GetComponent<InputButton>();
        }

        private void OnEnable()
        {
            _listenInputs = true;
            sensesData.SenseChangeEvent += HandleFirstSwitch;
        }
        private void OnDisable()
        {
            if (!_listenInputs) return;
            sensesData.SenseChangeEvent -= HandleFirstSwitch;
            _listenInputs = false;
        }

        private void HandleFirstSwitch(SensesState state)
        {
            sensesData.SenseChangeEvent -= HandleFirstSwitch;
            _listenInputs = false;
            StartCoroutine(WaitBeforeSend());
        }

        private IEnumerator WaitBeforeSend()
        {
            yield return new WaitForSeconds(timeBeforeHint);

            _resetButton.ForceDisable = false;
            SendPopup();
        }
    }
}
