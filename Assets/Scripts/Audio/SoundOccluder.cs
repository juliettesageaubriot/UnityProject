using System;
using Global;
using UnityEngine;
using Utils;

namespace Audio
{
    [RequireComponent(typeof(Pathfinder))]
    public class SoundOccluder : MonoBehaviour
    {
        [SerializeField] private ObstacleMap obstacleMap;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private AudioSource audioSource;
        private Pathfinder _pathfinder;
        private float _originVolume;

        private void Awake()
        {
            _pathfinder = GetComponent<Pathfinder>();
            _originVolume = audioSource.volume;
        }

        private void Start()
        { ComputeOcclusion(); }
        private void OnEnable()
        { obstacleMap.OnCleanArray += ComputeOcclusion; }
        private void OnDisable()
        { obstacleMap.OnCleanArray -= ComputeOcclusion; }

        private void ComputeOcclusion()
        {
            _pathfinder.ComputeDistanceMap();
            Debug.Log(_pathfinder.GetDistance(playerTransform.position));
            audioSource.volume = _pathfinder.GetDistance(playerTransform.position) == -1 ? 0f : _originVolume;
        }
    }
}