using Global;
using UI.Popup;
using UnityEngine;

namespace UI
{
    public class PopupSender : MonoBehaviour
    {
        [SerializeField] protected PopupBus bus;
        [SerializeField] protected PopupParameters popupParameters;

        public void SendPopup(PopupParameters overrideParams)
        {
            bus.SendPopup(overrideParams);
        }
        public void SendPopup()
        {
            bus.SendPopup(popupParameters);
        }
    }
}