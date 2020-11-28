using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Global;
using UnityEngine;

namespace UI.Popup
{
    public class PopupGenerator : MonoBehaviour
    {
        [SerializeField] private PopupBus bus;

        private Dictionary<AbstractPopup, PopupParameters> _popups = new Dictionary<AbstractPopup, PopupParameters>();
        
        private void OnEnable()
        { bus.PopupEvent += HandlePopup; }
        private void OnDisable()
        { bus.PopupEvent -= HandlePopup; }

        private void HandlePopup(PopupParameters popupParams)
        {
            StartCoroutine(DestroyAllAndGenerate(popupParams));
        }

        private IEnumerator DestroyAllAndGenerate(PopupParameters popupParams)
        {
            foreach (var popupable in _popups)
                if (popupable.Value.disappearBeforeNext == DisappearBeforeNext.Always)
                    popupable.Key.PopOut();
            
            while (
                popupParams.waitForPreviousDisappear
                && _popups.Count(
                    kvp =>
                        kvp.Value.disappearBeforeNext == DisappearBeforeNext.Always
                        ) > 0
            ) {
                yield return new WaitForSeconds(0.1f);
            }

            Generate(popupParams);
        }

        private void Generate(PopupParameters popupParams)
        {
            var popup = Instantiate(popupParams.popup, transform);
            popup.name = popupParams.popupName;

            var popupComponent = popup.GetComponent<AbstractPopup>();
            if (popupComponent == null)
                throw new NullReferenceException("There is no AbstractPopup component on object " + popup.name);
            
            popupComponent.InitPopup(this);

            _popups.Add(popupComponent, popupParams);

            if (popupParams.autoRemove)
                StartCoroutine(DOAutoDestroy(popupParams.autoRemoveDelay, popupComponent));
        }

        public void RemoveOneFromList(AbstractPopup popup)
        {
            if (!_popups.ContainsKey(popup)) return;
            bus.HasDestroyPopup(_popups[popup]);
            _popups.Remove(popup);
            Destroy(popup.gameObject);
        }

        private IEnumerator DOAutoDestroy(float delay, AbstractPopup popup)
        {
            yield return new WaitForSeconds(delay);
            if (_popups.ContainsKey(popup)) popup.PopOut();
        }
    }
}
