using System;
using Player;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(UIFade))]
    public class BlindBackground : MonoBehaviour
    {
        [SerializeField] private PlayerSensesData playerSensesData;
        private UIFade _fade;

        private void Awake()
        {
            _fade = GetComponent<UIFade>();
        }

        private void OnEnable()
        {
            playerSensesData.SenseChangeEvent += UpdateBackground;
            playerSensesData.SenseInitEvent += UpdateBackground;
        }

        private void OnDisable()
        {
            playerSensesData.SenseChangeEvent -= UpdateBackground;
            playerSensesData.SenseInitEvent -= UpdateBackground;
        }

        private void UpdateBackground(SensesState state)
        {
            if (state == SensesState.Blind)
                _fade.FadeIn();
            else
                _fade.FadeOut();
        }
    }
}
