using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FuelIndicator : MonoBehaviour
    {
        [SerializeField] private PlayerSensesData playerData;
        [SerializeField] private GameObject fuelIconPrefab;
        private readonly Stack<GameObject> _fuelIcons = new Stack<GameObject>();

        private void OnEnable() {
            playerData.FuelInitEvent += UpdateIcons;
            playerData.FuelChangeEvent += UpdateIcons;
        }
        private void OnDisable() {
            playerData.FuelInitEvent -= UpdateIcons;
            playerData.FuelChangeEvent -= UpdateIcons;
        }

        private void UpdateIcons(int newAmount)
        {
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
        
            // Rebuild layout to force horizontal layout group to do his job
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
        }

        private void AddOneIcon()
        {
            _fuelIcons.Push(Instantiate(fuelIconPrefab, transform));
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
            var maxDuration = Mathf.Max(move.MoveDuration, fade.FadeDuration);
            move.MoveOut();
            fade.FadeOut();
            yield return new WaitForSeconds(maxDuration);
            Destroy(icon);
        }
    }
}
