using System;
using UnityEngine;

namespace UI.Popup
{
    public abstract class AbstractPopup : MonoBehaviour, IPopupable
    {
        private PopupGenerator _generator;
        private bool _isPopingOut;

        public bool IsPopingOut => _isPopingOut;

        protected virtual void Start()
        {
            PopIn();
        }

        public void InitPopup(PopupGenerator generator)
        {
            _generator = generator;
        }

        public abstract void PopIn();

        public virtual void PopOut()
        {
            _isPopingOut = true;
        }

        protected void DestroyPopup()
        {
            _generator.RemoveOneFromList(this);
        }
    }
}