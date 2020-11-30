using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Interactables
{
    public class TrappedSlab : PlayerKiller
    {
        [SerializeField] private SingleUnityLayer playerLayer;
        [SerializeField] private Sprite brokenSprite;
        [SerializeField] private UnityEvent onSlabBreak;

        private SpriteRenderer _renderer;

        private void Start()
        {
            if (onSlabBreak == null)
                onSlabBreak = new UnityEvent();
            _renderer = GetComponent<SpriteRenderer>();
        }

        protected override void OnKill()
        {
            onSlabBreak.Invoke();
            BreakSlab();
        }

        public void BreakSlab()
        {
            _renderer.sprite = brokenSprite;
        }
        
        
    }
}
