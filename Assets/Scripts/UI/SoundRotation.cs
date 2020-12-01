using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace UI
{
    public class SoundRotation : MonoBehaviour
    {
        [SerializeField] [Range(10.0f, 100.0f)] private float rotationSpeed = 50f;
        [SerializeField] [Range(0f, 1f)] private float startRotation = 0f;
        [SerializeField] private float distance = 200f;
        [SerializeField] private GameObject emitter;
        private float _rotation;

        private void Start()
        {
            _rotation = startRotation * Mathf.PI * 2f;
        }

        private void FixedUpdate()
        {
            _rotation += rotationSpeed * Time.deltaTime / 50f;
            var newPosition = new Vector2(Mathf.Cos(_rotation) * distance, Mathf.Sin(_rotation) * distance);
            emitter.transform.position = gameObject.transform.TransformPoint(newPosition);
        }
    }
}