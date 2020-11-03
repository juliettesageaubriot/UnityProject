using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class SnapshotManager : MonoBehaviour
    {
        public static SnapshotManager Instance { get; private set; }
        public static bool IsReady { get; private set; }
        
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private AudioMixerSnapshot muffleSnapshot;
        [SerializeField] private AudioMixerSnapshot clearSnapshot;
        
        [Range(0f, 2f)]
        [SerializeField] private float transitionDuration = .8f;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            IsReady = true;
        }
        
        private void OnDestroy()
        {
            IsReady = false;
        }
        
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


