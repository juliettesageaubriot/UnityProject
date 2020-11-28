using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Global;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FuelIndicator : MonoBehaviour
    {
        [SerializeField] private PlayerSensesData playerData;
        [SerializeField] private GameObject fuelIconPrefab;
        [SerializeField] private float delayIconAnim;
        [SerializeField] private FuelUIAnimator fuelAnimator;
        private readonly Stack<GameObject> _fuelIcons = new Stack<GameObject>();

        private GameObject _placeholderFuelIcon;

        private void OnEnable() {
            playerData.FuelInitEvent += UpdateIcons;
            playerData.FuelChangeEvent += UpdateIcons;
            fuelAnimator.AnimationEndEvent += EnableLastIcon;
        }
        private void OnDisable() {
            playerData.FuelInitEvent -= UpdateIcons;
            playerData.FuelChangeEvent -= UpdateIcons;
            fuelAnimator.AnimationEndEvent -= EnableLastIcon;
        }

        private void UpdateIcons(int newAmount)
        {
            if (fuelAnimator.IsAnimating) fuelAnimator.InterruptAnim();
            
            var fuelCount = _fuelIcons.Count;
            if (newAmount == fuelCount) return;
        
            if (newAmount > fuelCount)
                // Add several
                for (var i = 0; i < newAmount - fuelCount; i++)
                    AddOneIcon();
            else
                // Remove several
                for (var i = 0; i < fuelCount - newAmount; i++)
                    RemoveOneIcon();

            if (_fuelIcons.Count > 0)
                fuelAnimator.FuelDestinationTransform = (RectTransform)_fuelIcons.Last().transform;
        
            // Rebuild layout to force horizontal layout group to do his job
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
        }

        private void EnableLastIcon()
        {
            if (_fuelIcons.Count > 0)
                _fuelIcons.Last().GetComponent<UIFade>().FadeIn(0f);
        }


        private void AddOneIcon()
        {
            var lastIcon = Instantiate(fuelIconPrefab, transform);
            var fade = lastIcon.GetComponent<UIFade>();
            fade.FadeOut(0f);
            _fuelIcons.Push(lastIcon);
        }

        private void RemoveOneIcon()
        {
            StartCoroutine(RemoveAnim());
        }

        private IEnumerator RemoveAnim()
        {
            var icon = _fuelIcons.Pop();
            var move = icon.GetComponent<UIMove>();
            var fade = icon.GetComponent<UIFade>();
            yield return new WaitForSeconds(delayIconAnim);
            var maxDuration = Mathf.Max(move.MoveDuration, fade.FadeDuration);
            move.MoveOut();
            fade.FadeOut();
            yield return new WaitForSeconds(maxDuration);
            Destroy(icon);
        }
    }
}
