using System;
using UnityEngine;

namespace UI.Popup
{
    public abstract class AbstractPopup : MonoBehaviour, IPopupable
    {
        private PopupGenerator _generator;

        private void Start()
        {
            PopIn();
        }

        public void InitPopup(PopupGenerator generator)
        {
            _generator = generator;
        }

        public abstract void PopIn();
        public abstract void PopOut();

        protected void DestroyPopup()
        {
            _generator.RemoveOneFromList(this);
        }
    }
}