using System;
using System.Collections.Generic;
using Global.Input;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using InputActionDictionnary = System.Collections.Generic.Dictionary<System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>, System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>>;

namespace Global
{
    [CreateAssetMenu(fileName = "Input", menuName = "ScriptableObjects/InputData", order = 12)]
    public class InputData : ScriptableObject
    {
        [SerializeField] private string inputName;
        public string InputName => inputName;
        
        [SerializeField] private bool isEnabled = true;
        public bool Can => isEnabled;
        
        [CanBeNull]
        public InputAction Event => InputManager.IsReady ? InputManager.ActionMaps.Player.Get()[inputName] : null;

        private InputActionDictionnary callbackDictionary = new InputActionDictionnary();
        
        public void SetEnable(bool newEnable)
        {
            isEnabled = newEnable;
        }

        public void Enable() { isEnabled = true; }
        public void Disable() { isEnabled = false; }

        public void AddListener(Action<InputAction.CallbackContext> callback)
        {
            void NewCallback(InputAction.CallbackContext ctx)
            {
                if (Can) callback(ctx);
            }

            if(Event != null) Event.performed += NewCallback;
            callbackDictionary.Add(callback, NewCallback);
        }
        
        public void RemoveListener(Action<InputAction.CallbackContext> callback)
        {
            var storedCallback = callbackDictionary[callback];
            callbackDictionary.Remove(callback);
            if(Event != null) Event.performed -= storedCallback;
        }
    }
}
