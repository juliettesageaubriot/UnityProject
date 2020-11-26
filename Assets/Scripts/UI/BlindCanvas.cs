using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Video;

namespace UI
{
    public class BlindCanvas : MonoBehaviour
    {
        [SerializeField] private PlayerSensesData sensesData;
        [SerializeField] private PlayerInputData inputData;
        [SerializeField] private UIFade mainBlindCanvas;
        [SerializeField] private UIFade cinematicPanel;
        [SerializeField] private VideoPlayer cinematic;
        [SerializeField] private VideoClip toBlindClip;
        [SerializeField] private VideoClip toSightedClip;

        private void OnEnable()
        { sensesData.SenseInitEvent += InitCanvas; sensesData.SenseChangeEvent += PlayCinematic; }

        private void OnDisable()
        { sensesData.SenseInitEvent -= InitCanvas; sensesData.SenseChangeEvent -= PlayCinematic; }

        public void InitCanvas(SensesState state)
        {
            if(state == SensesState.Blind) mainBlindCanvas.FadeIn();
            else mainBlindCanvas.FadeOut();
        }

        private void PlayCinematic(SensesState state)
        {
            if (state == SensesState.Blind) ToBlind();
            else ToSighted();
        }

        public void ToBlind()
        {
            BlindTransition();
        }
        
        public void ToSighted()
        {
            SightedTransition();
        }

        private void BlindTransition()
        {
            mainBlindCanvas.FadeIn();
            cinematic.clip = toBlindClip;
            
            inputData.DisableAll();
            cinematic.frame = 0;
            
            cinematicPanel.FadeIn(0f);
            cinematic.Play();
            StartCoroutine(WaitForTransition(() => {
                cinematicPanel.FadeOut();
                inputData.EnableAll();
            }));
        }

        private void SightedTransition()
        {
            cinematic.clip = toSightedClip;

            inputData.DisableAll();
            cinematic.frame = 0;
            
            cinematicPanel.FadeIn(0f);
            cinematic.Play();
            StartCoroutine(WaitForTransition(() => {
                cinematicPanel.FadeOut();
                inputData.EnableAll();
                mainBlindCanvas.FadeOut();
            }));
        }

        private IEnumerator WaitForTransition(Action callback)
        {
            yield return new WaitForSeconds((float)toBlindClip.length);
            callback();
        }
    }
}
