﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class UICharacterDelay : MonoBehaviour
    {
        private static char[] _pauseCharacters = { '.', '?', '!' };
        
        [SerializeField] private float delay = 0.1f; 
        [SerializeField] private float pointDelay = 0.1f; 
        private TextMeshProUGUI _textMeshPro;
        [SerializeField] private bool animateAtStart;
        [SerializeField] private bool cleanAtStart = true;
        [SerializeField] private bool useTMPText = true;

        [SerializeField] private UnityEvent animEndEvent;

        private char[] _characters;
        private Coroutine _animateCoroutine;
        
        // Start is called before the first frame update

        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        void Start()
        {
            if (animEndEvent == null) animEndEvent = new UnityEvent();
            if (useTMPText) SetText(_textMeshPro.text);
            if (cleanAtStart) CleanText();
            if (animateAtStart) Animate();
        }

        public void SetText(string text)
        {
            _characters = text.ToCharArray();
        }

        public void CleanText()
        {
            _textMeshPro.text = "";
        }

        public void Animate()
        {
            _animateCoroutine = StartCoroutine(DOAnimate());
        }

        public void CompleteAnimation()
        {
            _textMeshPro.text = new string(_characters);
            StopAnimate();
            animEndEvent.Invoke();
        }

        public void StopAnimate()
        {
            if (_animateCoroutine != null) StopCoroutine(_animateCoroutine);
        }

        private void OnDisable()
        {
            StopAnimate();
        }

        private IEnumerator DOAnimate()
        {
            foreach (var character in _characters)
            {
                if (char.IsLetter(character) || char.IsDigit(character)) yield return new WaitForSeconds(delay);
                _textMeshPro.text += character;
                if (Array.Exists(_pauseCharacters, c => c == character)) yield return new WaitForSeconds(pointDelay);
            }
            animEndEvent.Invoke();
        }
    }
}
