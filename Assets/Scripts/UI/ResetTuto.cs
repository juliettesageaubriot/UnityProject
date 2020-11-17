using System.Collections;
using Player;
using UnityEngine;

namespace UI
{
    public class ResetTuto : MonoBehaviour
    {
        [SerializeField] private PlayerInputData inputData;
        [Space(10)]
        [SerializeField] private float timeBeforeHint = 5f;
        [SerializeField] private float timeShowHint = 10f;
        [SerializeField] private float timeBeforeButton = 2f;
        [Space(10)]
        [SerializeField] private UIFade hint;
        [SerializeField] private UIFade button;

        private void Start()
        {
            inputData.SetResetEnable(false);
        }

        public void StartResetTuto()
        {
            StartCoroutine(ShowHint());
        }

        private IEnumerator ShowHint()
        {
            yield return new WaitForSeconds(timeBeforeHint);
            inputData.SetResetEnable(true);
            hint.FadeIn();
            yield return new WaitForSeconds(timeBeforeButton);
            button.FadeIn();
            yield return new WaitForSeconds(Mathf.Abs(timeShowHint - timeBeforeButton));
            hint.FadeOut();
        }
    }
}
