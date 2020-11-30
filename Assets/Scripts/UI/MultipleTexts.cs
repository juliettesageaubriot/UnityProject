using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(UICharacterDelay))]
    public class MultipleTexts : MonoBehaviour
    {
        [TextArea(1, 10)]
        [SerializeField] private string[] texts;
        private int _currentText = -1;
        private bool _isAnimating;

        public bool IsAnimating
        {
            get => _isAnimating;
            set => _isAnimating = value;
        }
    
        private UICharacterDelay _uiCharacterDelay;

        private void Awake()
        {
            _uiCharacterDelay = GetComponent<UICharacterDelay>();
        }

        public bool IsLastText()
        {
            return _currentText == texts.Length - 1;
        }

        public void PlayNextText()
        {
            if (IsLastText()) return;
            IsAnimating = true;
            _currentText++;
            _uiCharacterDelay.StopAnimate();
            _uiCharacterDelay.CleanText();
            _uiCharacterDelay.SetText(texts[_currentText]);
            _uiCharacterDelay.Animate();
        }

        public void CompleteCurrentText()
        {
            if (IsAnimating) _uiCharacterDelay.CompleteAnimation();
        }
    }
}
