using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace UI
{
    public class SoundRotation : MonoBehaviour
    {
        [SerializeField] [Range(10.0f, 100.0f)] private float rotationSpeed = 50.0f;
        private void FixedUpdate()
        {
            gameObject.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }
}