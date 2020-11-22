using System.Collections;
using Global.Input;
using Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utils;

namespace UI
{
    [RequireComponent(typeof(EventSequence))]
    public class FuelTuto : MonoBehaviour
    {
        [SerializeField] private Button tutoPanel;
        [SerializeField] private PlayerSensesData playerSenses;
        [SerializeField] private UnityEvent onStartTuto;
        [SerializeField] private UnityEvent onFirstSwitch;

        private bool _isListeningInputs;
        private EventSequence _endSequence;

        private void Start()
        {
            _endSequence = GetComponent<EventSequence>();
            playerSenses.SenseChangeEvent += HandleFirstSwitch;
            if (onStartTuto == null)
                onStartTuto = new UnityEvent();
            if (onFirstSwitch == null)
                onFirstSwitch = new UnityEvent();
        }

        public void StartTuto()
        {
            onStartTuto.Invoke();
            _isListeningInputs = true;
            tutoPanel.onClick.AddListener(HandlePassTuto);
            if(InputManager.IsReady) InputManager.ActionMaps.Player.Switch.performed += HandlePassTuto;
        }


        // Overload for input events
        private void HandlePassTuto(InputAction.CallbackContext obj)
        { HandlePassTuto(); }
        
        private void HandlePassTuto()
        {
            _isListeningInputs = false;
            
            _endSequence.StartSequence();
            
            tutoPanel.onClick.RemoveListener(HandlePassTuto);
            if(InputManager.IsReady) InputManager.ActionMaps.Player.Switch.performed -= HandlePassTuto;
        }

        private void HandleFirstSwitch(SensesState _)
        {
            playerSenses.SenseChangeEvent -= HandleFirstSwitch;
            onFirstSwitch.Invoke();
            _endSequence.StopSequence();
        }
        
        private void OnDisable()
        {
            playerSenses.SenseChangeEvent -= HandleFirstSwitch;
            if (_isListeningInputs)
            {
                tutoPanel.onClick.RemoveListener(HandlePassTuto);
                if(InputManager.IsReady) InputManager.ActionMaps.Player.Switch.performed -= HandlePassTuto;
            }
        }
    }
}
