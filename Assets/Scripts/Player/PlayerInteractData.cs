using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerInteractData", menuName = "ScriptableObjects/PlayerInteractData", order = 10)]
    public class PlayerInteractData : ScriptableObject
    {
        public delegate void CanInteractChangeHandler(bool canInteract);
        public event CanInteractChangeHandler CanInteractChange;

        public delegate void CheckInteractHandler();
        public event CheckInteractHandler CheckInteractEvent;
        
        public delegate void InteractHandler();
        public event InteractHandler InteractEvent;

        public void ShouldCheckInteract()
        {
            CheckInteractEvent?.Invoke();
        }
        
        public void ShouldInteract()
        {
            InteractEvent?.Invoke();
        }
        
        public void CanInteractHasChanged(bool canChange)
        {
            CanInteractChange?.Invoke(canChange);
        }
    }
}
