namespace UI.Popup
{
    public interface IPopupable
    {
        bool IsPopingOut { get; }
        void PopIn();
        void PopOut();
    }
}