using System;
using System.Collections;
using Player;
using UnityEngine;
using Utils;

namespace Interactables
{
    public class TrappedSlab : MonoBehaviour
    {
        [SerializeField] private SingleUnityLayer playerLayer;
        [SerializeField] private Sprite brokenSprite;
        [SerializeField] private SingleUnityLayer brokenLayer;

        private SpriteRenderer _renderer;
        private Collider2D _collider2D;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != playerLayer.LayerIndex) return;
            
            _renderer.sprite = brokenSprite;
            gameObject.layer = brokenLayer.LayerIndex;
            _collider2D.isTrigger = false;
            
            other.GetComponent<PlayerController>().enabled = false;
        }
    }
}
