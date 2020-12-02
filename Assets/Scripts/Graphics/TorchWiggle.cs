using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

namespace Graphics
{
    public class TorchWiggle : MonoBehaviour
    {
        [SerializeField] private float positionAmplitude = 1f;
        [SerializeField] private float intensityAmplitude = 0.05f;
        [SerializeField] private float outerRadiusAmplitude = 0.05f;
        [SerializeField] private float speed = 1f;
        private float _seed;
        private Vector3 _defaultPosition;
        private float _defaultIntensity;
        private float _defaultOuterRadius;
        private Light2D _light2D;

        private void Awake()
        {
            _light2D = GetComponent<Light2D>();
            _defaultIntensity = _light2D.intensity;
            _defaultOuterRadius = _light2D.pointLightOuterRadius;
            _defaultPosition = transform.position;
            _seed = Random.Range(-10000, 10000);
        }
        
        private void Update()
        {
            var intensityRandom = Mathf.PerlinNoise(Time.time * speed, -_seed);
            var outerRadiusRandom = Mathf.PerlinNoise(-_seed, Time.time * speed);
            var xRandom = Mathf.PerlinNoise(Time.time * speed, _seed);
            var yRandom = Mathf.PerlinNoise(_seed, Time.time * speed);
            var offset = (new Vector2(xRandom, yRandom) - Vector2.one / 2f) * (positionAmplitude * 2f);
            transform.position = _defaultPosition + (Vector3)offset;
            _light2D.intensity = _defaultIntensity + (intensityRandom - 0.5f) * 2f * intensityAmplitude;
            _light2D.pointLightOuterRadius = _defaultOuterRadius + (outerRadiusRandom - 0.5f) * 2f * outerRadiusAmplitude;

        }
    }
}
