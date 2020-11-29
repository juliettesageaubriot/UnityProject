using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UICharacterDelay : MonoBehaviour
    {
        [SerializeField] private float delay = 0.1f; 
        private TextMeshProUGUI _textMeshPro;
        [SerializeField] private bool animateAtStart;

        private char[] _characters;
        
        // Start is called before the first frame update
        void Start()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
            _characters = _textMeshPro.text.ToCharArray();
            _textMeshPro.text = "";
            if (animateAtStart) Animate();
        }

        public void Animate()
        {
            StartCoroutine(DOAnimate());
        }

        private IEnumerator DOAnimate()
        {
            foreach (var character in _characters)
            {
                if (char.IsLetter(character) || char.IsDigit(character)) yield return new WaitForSeconds(delay);
                _textMeshPro.text += character;
            }
        }
    }
}
