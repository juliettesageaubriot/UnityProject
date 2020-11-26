using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    [Serializable]
    public struct MoveState
    {
        public Vector2 position;
    }

    [RequireComponent(typeof(RectTransform))]
    public class UIMove : MonoBehaviour
    {
        [SerializeField] private float moveDuration;
        public float MoveDuration => moveDuration;

        [SerializeField]
        private MoveState inState = new MoveState() { position = Vector2.zero };
        [SerializeField]
        private MoveState outState = new MoveState() { position = Vector2.zero };

        [SerializeField] private bool startIn = true;
        [SerializeField] private bool offsetValues = false;
        private RectTransform _rectTransform;
    

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            Fade(startIn ? inState : outState, 0f);
        }

        public void MoveIn() { Fade(inState); }
        public void MoveIn(float duration) { Fade(inState, duration); }
        public void MoveOut() { Fade(outState); }
        public void MoveOut(float duration) { Fade(outState, duration); }

        private void Fade(MoveState state)
        {
            _rectTransform.DOMove(offsetValues ? (Vector2)_rectTransform.position + state.position : state.position, moveDuration);
        }
        private void Fade(MoveState state, float duration)
        {
            _rectTransform.DOMove(offsetValues ? (Vector2)_rectTransform.position + state.position : state.position, duration);
        }
    }
}