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
        [SerializeField] private UIFade fuelTutoPanel;
        [SerializeField] private UIFade switchButton;
        [SerializeField] private UIFade fuelIndicator;
        [SerializeField] private UIFade ctaSwitch;
        [Space(10)]
        [SerializeField] private PlayerInputData inputData;
        [SerializeField] private float timeBeforeSwitchEnable = 2.5f;
        [Space(10)]
        [SerializeField] private UnityEvent onTutoEndEvent;

        private bool _isListeningInputs;

        private void Start()
        {
            if (onTutoEndEvent == null)
                onTutoEndEvent = new UnityEvent();
        }

        public void StartTuto()
        {
            fuelTutoPanel.FadeIn();
            inputData.SetMoveEnable(false);
            inputData.SetSwitchEnable(false);

            StartCoroutine(ListenInputs());
        }

        public void EndTuto()
        {
            fuelTutoPanel.FadeOut();
            inputData.SetMoveEnable(true);
            onTutoEndEvent.Invoke();
        }

        private IEnumerator ListenInputs()
        {
            yield return new WaitForSeconds(timeBeforeSwitchEnable);
            ctaSwitch.FadeIn();
            _isListeningInputs = true;
            inputData.SetSwitchEnable(true);
            if (InputManager.IsReady) InputManager.ActionMaps.Player.Switch.performed += HandleInputs;
            switchButton.FadeIn();
            fuelIndicator.FadeIn();
        }

        private void HandleInputs(InputAction.CallbackContext obj)
        {
            _isListeningInputs = false;
            if (InputManager.IsReady) InputManager.ActionMaps.Player.Switch.performed -= HandleInputs;
            EndTuto();
        }

        private void OnDisable()
        {
            inputData.SetMoveEnable(true);
            inputData.SetSwitchEnable(true);
            if (InputManager.IsReady && _isListeningInputs)
                InputManager.ActionMaps.Player.Switch.performed -= HandleInputs;
        }
    }
}
