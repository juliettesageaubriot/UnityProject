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
        [SerializeField] private SingleUnityLayer brokenLayer;
        [SerializeField] private UnityEvent onSlabBreak;

        private SpriteRenderer _renderer;
        private Collider2D _collider2D;

        private void Start()
        {
            if (onSlabBreak == null)
                onSlabBreak = new UnityEvent();
            _renderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
        }

        protected override void OnKill()
        {
            onSlabBreak.Invoke();
            
            _renderer.sprite = brokenSprite;
            gameObject.layer = brokenLayer.LayerIndex;
            _collider2D.isTrigger = false;
        }
    }
}
