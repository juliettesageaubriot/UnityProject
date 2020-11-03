using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class SnapshotManager : MonoBehaviour

    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private AudioMixerSnapshot muffleSnapshot;
        [SerializeField] private AudioMixerSnapshot clearSnapshot;
        
        [Range(0f, 2f)]
        [SerializeField] private float transitionDuration = .8f;

        public void UnmuffleSound()
        {
            var snapshots = new []{muffleSnapshot, clearSnapshot};
            var weight = new[] {0f, 1f};
        
            mixer.TransitionToSnapshots(snapshots, weight, transitionDuration);
        }
    
        public void MuffleSound()
        {
            var snapshots = new []{muffleSnapshot, clearSnapshot};
            var weight = new[] {1f, 0f};
        
            mixer.TransitionToSnapshots(snapshots, weight, transitionDuration);
        }
    }
}

