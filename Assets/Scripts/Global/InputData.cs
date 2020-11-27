using Global.Input;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Global
{
    [CreateAssetMenu(fileName = "Input", menuName = "ScriptableObjects/InputData", order = 12)]
    public class InputData : ScriptableObject
    {
        [SerializeField] private string inputName;
        public string InputName => inputName;
        
        [SerializeField] private bool isEnabled = true;

        public bool Can => isEnabled;

        public delegate void InputDelegate(InputAction.CallbackContext context);
        public event InputDelegate InputEvent;
        public delegate void ChangeEnableDelegate(bool isEnable);
        public event ChangeEnableDelegate ChangeEnableEvent;
        
        [CanBeNull]
        public InputAction BaseEvent => InputManager.IsReady ? InputManager.ActionMaps.Player.Get()[inputName] : null;

        public void ListenInput()
        {
            if (BaseEvent != null) BaseEvent.performed += InvokeSyntheticEvent;
        }

        public void SetEnable(bool newEnable)
        {
            ChangeEnableEvent?.Invoke(newEnable);
            isEnabled = newEnable;
        }

        public void Enable() { SetEnable(true); }
        public void Disable() { SetEnable(false);  }


        private void InvokeSyntheticEvent(InputAction.CallbackContext ctx)
        {
            if (Can) InputEvent?.Invoke(ctx);
        }
    }
}
