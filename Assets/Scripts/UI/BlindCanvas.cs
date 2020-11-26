using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace UI
{
    public class BlindCanvas : MonoBehaviour
    {
        [SerializeField] private UIFade mainBlindCanvas;
        [SerializeField] private UIFade cinematicPanel;
        [SerializeField] private VideoPlayer cinematic;
        [SerializeField] private VideoClip toBlindClip;
        [SerializeField] private VideoClip toSightedClip;

        private float TransitionDuration => Mathf.Max((float)toBlindClip.length, mainBlindCanvas.FadeDuration);

        public void ToBlind()
        {
            BlindTransition();
        }
        public void ToBlind(Action callback)
        {
            BlindTransition();
            StartCoroutine(WaitForTransition(TransitionDuration, callback));
        }
        
        public void ToSighted()
        {
            SightedTransition();
        }
        public void ToSighted(Action callback)
        {
            SightedTransition();
            StartCoroutine(WaitForTransition(TransitionDuration, callback));
        }

        private void BlindTransition()
        {
            mainBlindCanvas.FadeIn();
            cinematic.clip = toBlindClip;
            
            cinematic.frame = 0;
            cinematic.Play();
            cinematicPanel.FadeIn(0f);
            StartCoroutine(WaitForTransition(TransitionDuration, () =>
                { cinematicPanel.FadeOut(); }));
        }

        private void SightedTransition()
        {
            mainBlindCanvas.FadeOut();
            cinematic.clip = toSightedClip;

            cinematic.frame = 0;
            cinematic.Play();
            cinematicPanel.FadeIn(0f);
            StartCoroutine(WaitForTransition(TransitionDuration, () =>
                { cinematicPanel.FadeOut(); }));
        }

        private static IEnumerator WaitForTransition(float duration, Action callback)
        {
            yield return new WaitForSeconds(duration);
            callback();
        }
    }
}
