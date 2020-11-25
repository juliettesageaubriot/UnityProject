using System;
using Global;
using Player;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class SnapshotManager : MonoBehaviour
    {
        public static SnapshotManager Instance { get; private set; }
        public static bool IsReady { get; private set; }

        [SerializeField] private PlayerSensesData data;
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private AudioMixerSnapshot muffleSnapshot;
        [SerializeField] private AudioMixerSnapshot clearSnapshot;
        [SerializeField] private AudioMixerSnapshot pauseSnapshot;

        [Range(0f, 2f)] [SerializeField] private float transitionDuration = .8f;

        private AudioMixerSnapshot[] SnapshotArray => new[] {muffleSnapshot, clearSnapshot, pauseSnapshot};

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            IsReady = true;
        }

        private void OnDestroy() { IsReady = false; }

        private void OnEnable()
        {
            data.SenseInitEvent += OnInitSenses;
            data.SenseChangeEvent += OnChangeSense;
        }

        private void OnDisable()
        {
            data.SenseInitEvent -= OnInitSenses;
            data.SenseChangeEvent -= OnChangeSense;

        }

        private void OnInitSenses(SensesState state)
        {
            if (data.State == SensesState.Deaf) MuffleSound(0f);
            else UnmuffleSound(0f);

            mixer.GetInstanceID();
        }

        private void OnChangeSense(SensesState state)
        {
            if (state == SensesState.Deaf) MuffleSound();
            else UnmuffleSound();
        }

        public void UnmuffleSound()
        { TransitionSnapshot(new[] {0f, 1f, 0f}, transitionDuration); }
        public void UnmuffleSound(float duration)
        { TransitionSnapshot(new[] {0f, 1f, 0f}, duration); }

        public void MuffleSound()
        { TransitionSnapshot(new[] {1f, 0f, 0f}, transitionDuration); }
        public void MuffleSound(float duration)
        { TransitionSnapshot(new[] {1f, 0f, 0f}, duration); }

        public void PauseSound()
        { TransitionSnapshot(new[] {0f, 0f, 1f}, transitionDuration); }

         public void PauseSound(float duration)
         { TransitionSnapshot(new[] {0f, 0f, 1f}, duration); }
        
        private void TransitionSnapshot(float[] weight, float duration)
        { mixer.TransitionToSnapshots(SnapshotArray, weight, duration); }
    }
}


