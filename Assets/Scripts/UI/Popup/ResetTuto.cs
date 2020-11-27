using System.Collections;
using Global;
using UnityEngine;

namespace UI.Popup
{
    public class ResetTuto : AbstractPopup
    {
        [SerializeField] private UIFade fade;
        
        public override void PopIn()
        {
            fade.FadeIn();
        }

        public override void PopOut()
        {
            fade.FadeOut();
            StartCoroutine(WaitForDestroy());
        }

        private IEnumerator WaitForDestroy()
        {
            yield return new WaitForSeconds(fade.FadeDuration);
            DestroyPopup();
        }
    }
}
