using System;
using System.Collections;
using System.Collections.Generic;
using Global;
using UnityEngine;

namespace UI.Popup
{
    public class PopupGenerator : MonoBehaviour
    {
        [SerializeField] private PopupBus bus;

        private Dictionary<AbstractPopup, PopupParameters> _popups = new Dictionary<AbstractPopup, PopupParameters>();
        
        private void OnEnable()
        { bus.PopupEvent += Generate; }
        private void OnDisable()
        { bus.PopupEvent -= Generate; }

        private void Generate(PopupParameters popupParams)
        {
            var popup = Instantiate(popupParams.popup, transform);
            popup.name = popupParams.popupName;

            var popupComponent = popup.GetComponent<AbstractPopup>();
            if (popupComponent == null)
                throw new NullReferenceException("There is no AbstractPopup component on object " + popup.name);
            
            popupComponent.InitPopup(this);
            
            if (popupParams.removePrevious)
                foreach (var popupable in _popups)
                    popupable.Key.PopOut();

            _popups.Add(popupComponent, popupParams);

            if (popupParams.autoRemove)
                StartCoroutine(DOAutoDestroy(popupParams.autoRemoveDelay, popupComponent));
        }

        public void RemoveOneFromList(AbstractPopup popup)
        {
            bus.HasDestroyPopup(_popups[popup]);
            _popups.Remove(popup);
            Destroy(popup.gameObject);
        }

        private IEnumerator DOAutoDestroy(float delay, AbstractPopup popup)
        {
            yield return new WaitForSeconds(delay);
            popup.PopOut();
        }
    }
}
