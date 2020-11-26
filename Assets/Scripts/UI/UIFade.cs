using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    [Serializable]
    public struct FadeState
    {
        public float opacity;
        public bool interactable;
        public bool blocksRaycasts;
    }

    [RequireComponent(typeof(CanvasGroup))]
    public class UIFade : MonoBehaviour
    {
        [SerializeField] private float fadeDuration;
        public float FadeDuration => fadeDuration;

        [SerializeField]
        private FadeState hideState = new FadeState() {interactable = false, opacity = 0f, blocksRaycasts = false};
        [SerializeField]
        private FadeState visibleState = new FadeState() {interactable = true, opacity = 1f, blocksRaycasts = true};

        [SerializeField] private bool startVisible = true;
    
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            Fade(startVisible ? visibleState : hideState, 0f);
        }

        public void FadeIn() { Fade(visibleState); }
        public void FadeIn(float duration) { Fade(visibleState, duration); }
        public void FadeOut() { Fade(hideState); }
        public void FadeOut(float duration) { Fade(hideState, duration); }

        private void Fade(FadeState state)
        {
            _canvasGroup.DOFade(state.opacity, fadeDuration);
            _canvasGroup.interactable = state.interactable;
            _canvasGroup.blocksRaycasts = state.blocksRaycasts;
        }
        private void Fade(FadeState state, float duration)
        {
            _canvasGroup.DOFade(state.opacity, duration);
            _canvasGroup.interactable = state.interactable;
            _canvasGroup.blocksRaycasts = state.blocksRaycasts;
        }
    }
}