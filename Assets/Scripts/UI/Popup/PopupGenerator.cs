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
            if (
                (popupParams.skipIf == Skip.DifferentName
                 && _popups.Values.ToList().Exists(p => p.popupName != popupParams.popupName))
                || (popupParams.skipIf == Skip.SameName
                    && _popups.Values.ToList().Exists(p => p.popupName == popupParams.popupName))
            ) return;
            
            PopOutPrevious(popupParams);
            if (popupParams.waitForPreviousDisappear) StartCoroutine(WaitGenerate(popupParams));
            else Generate(popupParams);
        }
        
        private void PopOutPrevious(PopupParameters newPopup)
        {
            var toRemoveImmediate = new List<AbstractPopup>();
            foreach (var popupable in _popups)
            {
                if (!ShouldDisappear(popupable.Value, newPopup)) continue;
                if (newPopup.destroyImmediatePrevious) toRemoveImmediate.Add(popupable.Key);
                else if (!popupable.Key.IsPopingOut)
                    popupable.Key.PopOut();
            }
            
            foreach (var abstractPopup in toRemoveImmediate) 
                RemoveOneFromList(abstractPopup);
        }

        private bool ShouldDisappear(PopupParameters popup, PopupParameters newPopup)
        {
            return popup.disappearBeforeNext == DisappearBeforeNext.Always
                   || (
                       popup.disappearBeforeNext == DisappearBeforeNext.DifferentName
                       && popup.popupName != newPopup.popupName);
        }
        

        private IEnumerator WaitGenerate(PopupParameters popupParams)
        {
            while (_popups.Count(
                    kvp =>
                        ShouldDisappear(kvp.Value, popupParams)
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
            if (_popups.ContainsKey(popup) && !popup.IsPopingOut)
                popup.PopOut();
        }
    }
}
