using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Video;

namespace UI.Popup
{
    public class CinematicPopup : AbstractPopup
    {
        [SerializeField] private PlayerSensesData playerSensesData;
        [SerializeField] private PlayerInputData playerInputData;
        [SerializeField] private VideoClip toBlindClip;
        [SerializeField] private VideoClip toSightedClip;
        [SerializeField] private VideoPlayer videoPlayer;

        private void OnEnable()
        {
            videoPlayer.loopPointReached += HandleVideoEnd;
        }
        
        private void OnDisable()
        {
            videoPlayer.loopPointReached -= HandleVideoEnd;
        }

        private void HandleVideoEnd(VideoPlayer source)
        {  PopOut(); }

        public override void PopIn()
        {
            videoPlayer.clip = playerSensesData.State == SensesState.Blind ? toBlindClip : toSightedClip;
            videoPlayer.frame = 0;
            playerInputData.DisableAll();
        }

        public override void PopOut()
        {
            playerInputData.EnableAll();
            base.PopOut();
            DestroyPopup();
        }
    }
}
