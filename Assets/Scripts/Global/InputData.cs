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
        
        [CanBeNull]
        public InputAction BaseEvent => InputManager.IsReady ? InputManager.ActionMaps.Player.Get()[inputName] : null;

        public void ListenInput()
        {
            if (BaseEvent != null) BaseEvent.performed += InvokeSyntheticEvent;
        }

        public void SetEnable(bool newEnable)
        {
            isEnabled = newEnable;
        }

        public void Enable() { isEnabled = true; }
        public void Disable() { isEnabled = false;  }


        private void InvokeSyntheticEvent(InputAction.CallbackContext ctx)
        {
            if (Can) InputEvent?.Invoke(ctx);
        }
    }
}
