using System;
using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class DoorAnimator : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private string openAnimName;
        [SerializeField] private string closedAnimName;

        public void OpenImmediate()
        {
            _animator.Play(openAnimName, -1, 1f);
        }
        
        public void CloseImmediate()
        {
            _animator.Play(closedAnimName, -1, 1f);
        }

        public void Open()
        {
            _animator.Play(openAnimName);
        }
        
        public void Close()
        {
            _animator.Play(closedAnimName);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    }
}
