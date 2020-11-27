using UI;
using UI.Popup;
using UnityEngine;

namespace Global
{
    [CreateAssetMenu(fileName = "PopupBus", menuName = "ScriptableObjects/PopupBus", order = 1)]
    public class PopupBus : ScriptableObject
    {
        public delegate void PopupDelegate(PopupParameters popup);
        public event PopupDelegate PopupEvent;
        public event PopupDelegate DestroyPopupEvent;

        public void SendPopup(PopupParameters popup)
        {
            PopupEvent?.Invoke(popup);
        }

        public void HasDestroyPopup(PopupParameters popup)
        {
            DestroyPopupEvent?.Invoke(popup);
        }
    }
}
