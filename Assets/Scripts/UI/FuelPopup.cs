using System;
using System.Collections;
using Player;
using UnityEngine;

namespace UI
{
    public class FuelPopup : MonoBehaviour
    {
        [SerializeField] private PlayerSensesData sensesData;
        [SerializeField] private float fadeBackAfter = 3f;
        [SerializeField] private bool skipFirst;
        private UIFade _fade;
        private bool _isFirst = true;

        private void Start()
        {
            _fade = GetComponent<UIFade>();
        }

        private void OnEnable()
        { sensesData.FuelChangeEvent += OnFuelChange; }
        private void OnDisable()
        { sensesData.FuelChangeEvent -= OnFuelChange; }

        private void OnFuelChange(int newFuelAmount)
        {
            if (newFuelAmount < sensesData.FuelAmount) return;
            if (_isFirst && skipFirst)
            {
                _isFirst = false;
                return;
            }
            StartCoroutine(FadeBack());
        }

        private IEnumerator FadeBack()
        {
            _fade.FadeIn();
            yield return new WaitForSeconds(fadeBackAfter);
            _fade.FadeOut();
        }
    }
}
