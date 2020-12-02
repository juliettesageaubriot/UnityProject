using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Graphics
{
    [Serializable]
    public struct LightState
    {
        public float intensity;
        public float innerRadius;
        public float outerRadius;
    }
    [RequireComponent(typeof(Light2D))]
    public class LightFade : MonoBehaviour
    {
        [SerializeField] private float transitionDuration;

        [SerializeField]
        private LightState hideState = new LightState
            {intensity = 0f, innerRadius = 0f, outerRadius = 5f};

        [SerializeField] private LightState visibleState = new LightState
            {intensity = 1f, innerRadius = 0f, outerRadius = 5f};

        [SerializeField] private bool startVisible = true;
        [SerializeField] private bool currentStateAsVisible = true;
    
        private Light2D _light2D;

        private void Awake()
        {
            _light2D = GetComponent<Light2D>();
            if (currentStateAsVisible) visibleState = new LightState
            {
                intensity = _light2D.intensity,
                innerRadius = _light2D.pointLightInnerRadius,
                outerRadius = _light2D.pointLightOuterRadius
            };
        }

        private void Start()
        {
            Fade(startVisible ? visibleState : hideState, 0f);
        }

        public void FadeIn() { Fade(visibleState); }
        public void FadeIn(float duration) { Fade(visibleState, duration); }
        public void FadeOut() { Fade(hideState); }
        public void FadeOut(float duration) { Fade(hideState, duration); }

        private void Fade(LightState state)
        {
            Fade(state, transitionDuration);
        }
        private void Fade(LightState state, float duration)
        {
            DOTween.To(
                value => _light2D.intensity = value,
                _light2D.intensity,
                state.intensity,
                duration
            );
            DOTween.To(
                value => _light2D.pointLightInnerRadius = value,
                _light2D.pointLightInnerRadius,
                state.innerRadius,
                duration
            );
            DOTween.To(
                value => _light2D.pointLightOuterRadius = value,
                _light2D.pointLightOuterRadius,
                state.outerRadius,
                duration
            );
        }
    }
}
