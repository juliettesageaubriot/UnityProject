using UI;

namespace Interactables
{
    public class ActionPopupTrigger : PopupSender, IActionable
    {
        public void Action()
        {
            SendPopup();
        }

        public bool IsActionable()
        {
            return true;
        }
    }
}
