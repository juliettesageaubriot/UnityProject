using System.Collections;
using Global;
using Player;
using UnityEngine;

namespace UI
{
    public class ResetTuto : MonoBehaviour
    {
        [SerializeField] private InputData resetInput;
        [Space(10)]
        [SerializeField] private float timeBeforeHint = 5f;
        [SerializeField] private float timeShowHint = 10f;
        [SerializeField] private float timeBeforeButton = 2f;
        [Space(10)]
        [SerializeField] private UIFade hint;
        [SerializeField] private UIFade button;

        private void Start()
        {
            resetInput.Disable();
        }

        public void StartResetTuto()
        {
            StartCoroutine(ShowHint());
        }

        private IEnumerator ShowHint()
        {
            yield return new WaitForSeconds(timeBeforeHint);
            resetInput.Enable();
            hint.FadeIn();
            yield return new WaitForSeconds(timeBeforeButton);
            button.FadeIn();
            yield return new WaitForSeconds(Mathf.Abs(timeShowHint - timeBeforeButton));
            hint.FadeOut();
        }
    }
}
