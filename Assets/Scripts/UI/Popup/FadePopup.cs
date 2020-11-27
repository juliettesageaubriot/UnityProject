using System.Collections;
using UnityEngine;

namespace UI.Popup
{
    [RequireComponent(typeof(UIFade))]
    public class FadePopup : AbstractPopup
    {
        private UIFade _fade;
        protected override void Start()
        {
            _fade = GetComponent<UIFade>();
            base.Start();
        }

        public override void PopIn()
        {
            _fade.FadeIn();
        }

        public override void PopOut()
        {
            _fade.FadeOut();
            StartCoroutine(WaitForDestroy());
        }
        
        private IEnumerator WaitForDestroy()
        {
            yield return new WaitForSeconds(_fade.FadeDuration);
            DestroyPopup();
        }
    }
}