using UnityEngine;

namespace Player
{
    public struct InputTypes
    {
        public bool move;
        public bool senseSwitch;
        public bool reset;
    }
    
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerInputData", order = 10)]
    public class PlayerInputData : ScriptableObject
    {
        public InputTypes Can { get; private set; }
        
        public void Awake()
        {
            Can = new InputTypes {move = true, senseSwitch = true, reset = true};;
        }

        public void SetMoveEnable(bool newVal)
        {
            var state = Can;
            state.move = newVal;
            Can = state;
        }
        
        public void SetSwitchEnable(bool newVal)
        {
            var state = Can;
            state.senseSwitch = newVal;
            Can = state;
        }
        
        public void SetResetEnable(bool newVal)
        {
            var state = Can;
            state.reset = newVal;
            Can = state;
        }
        
    }
}
