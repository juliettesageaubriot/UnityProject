using System.Collections;
using Global.Input;
using Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UI
{
    public class FuelTuto : MonoBehaviour
    {
        [SerializeField] private PlayerSensesData playerSenses;
        [SerializeField] private UnityEvent onTutoSwitch;

        private bool _isListeningInputs;

        private void Start()
        {
            if (onTutoSwitch == null)
                onTutoSwitch = new UnityEvent();
        }

        public void WaitForSwitch()
        {
            _isListeningInputs = true;
            playerSenses.SenseChangeEvent += HandleSwitch;
        }

        private void HandleSwitch(SensesState _)
        {
            _isListeningInputs = false;
            playerSenses.SenseChangeEvent -= HandleSwitch;
            onTutoSwitch.Invoke();
        }

        private void OnDisable()
        {
            if (_isListeningInputs)
                playerSenses.SenseChangeEvent -= HandleSwitch;
        }
    }
}
