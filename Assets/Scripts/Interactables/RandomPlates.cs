using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Interactables
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class RandomPlates: MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length - 1)];
        }
    }
}