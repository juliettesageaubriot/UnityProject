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
        
        public override void PopIn()
        {
            videoPlayer.clip = playerSensesData.State == SensesState.Blind ? toBlindClip : toSightedClip;
            videoPlayer.frame = 0;
            playerInputData.DisableAll();
            StartCoroutine(PlayVideoBeforePopOut());
        }

        public override void PopOut()
        {
            base.PopOut();
            DestroyPopup();
        }

        private IEnumerator PlayVideoBeforePopOut()
        {
            yield return new WaitForSecondsRealtime((float)videoPlayer.length);
            playerInputData.EnableAll();
            PopOut();
        }
    }
}
