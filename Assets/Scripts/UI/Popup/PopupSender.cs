using Global;
using UI.Popup;
using UnityEngine;

namespace UI
{
    public class PopupSender : MonoBehaviour
    {
        [SerializeField] protected PopupBus bus;
        [SerializeField] private PopupParameters popupParameters;

        public void SendPopup()
        {
            bus.SendPopup(popupParameters);
        }
    }
}