using System;
using Global;
using Player;
using UnityEngine;
using Utils;

namespace Audio
{
    public class SoundOccluder : MonoBehaviour
    {
        [SerializeField] private PlayerPositionData positionData;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Pathfinder pathfinder;
        private float _originVolume;

        private void Awake()
        {
            _originVolume = audioSource.volume;
        }

        public void OccludeSound()
        {
            audioSource.volume = pathfinder.GetDistance(positionData.Position) == -1 ? 0f : _originVolume;
        }
    }
}