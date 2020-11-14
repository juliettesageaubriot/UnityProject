using System;
using Global;
using UnityEngine;

namespace Player
{
    [Serializable]
    public struct InputTypes
    {
        public bool move;
        public bool senseSwitch;
        public bool reset;
    }
    
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerInputData", order = 10)]
    public class PlayerInputData : ScriptableObject
    {
        [SerializeField] private InputTypes inputEnable;
        [SerializeField] private ScriptableSceneManager sceneManager;

        public InputTypes Can => inputEnable;

        public void SetMoveEnable(bool newVal)
        {
            var state = inputEnable;
            state.move = newVal;
            inputEnable = state;
        }
        
        public void SetSwitchEnable(bool newVal)
        {
            var state = inputEnable;
            state.senseSwitch = newVal;
            inputEnable = state;
        }
        
        public void SetResetEnable(bool newVal)
        {
            var state = inputEnable;
            state.reset = newVal;
            inputEnable = state;
        }

        private void OnEnable()
        { sceneManager.BeforeSceneChangeEvent += EnableAll; }

        private void OnDisable()
        { sceneManager.BeforeSceneChangeEvent -= EnableAll; }

        private void EnableAll()
        {
            inputEnable = new InputTypes { move = true, reset = true, senseSwitch = true };
        }
    }
}
