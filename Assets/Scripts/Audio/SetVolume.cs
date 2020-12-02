using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class SetVolume : MonoBehaviour
    {
        private SnapshotManager _snapshotManager;
        private void Start()
        {
            if (SnapshotManager.IsReady) _snapshotManager = SnapshotManager.Instance;;
        }

        public void SetLevel(float sliderValue)
        {
            _snapshotManager.UpdateVolume(sliderValue);
        }

    }
}