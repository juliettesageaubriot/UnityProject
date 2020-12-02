using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Interactables
{
    [RequireComponent(typeof(Animator))]
    public class RandomPlatesAnimation: MonoBehaviour
    {
        [SerializeField] private string[] animNames;

        private Animator _animator;
        private string _animName;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _animName = animNames[Random.Range(0, animNames.Length - 1)];
            _animator.speed = 0f;
            _animator.Play(_animName, -1, 0f);
        }

        public void PlayAnim()
        {
            _animator.speed = 1f;
            _animator.Play(_animName);
        }
        
    }
}