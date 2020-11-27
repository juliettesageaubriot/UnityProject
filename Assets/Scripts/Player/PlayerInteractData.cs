using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerInteractData", menuName = "ScriptableObjects/PlayerInteractData", order = 10)]
    public class PlayerInteractData : ScriptableObject
    {
        public delegate void CanInteractChangeHandler(bool canInteract);

        public event CanInteractChangeHandler CanInteractChange;

        public void CanInteractHasChanged(bool canChange)
        {
            CanInteractChange?.Invoke(canChange);
        }
    }
}
