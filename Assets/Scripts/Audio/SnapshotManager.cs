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

        [Range(0f, 2f)] [SerializeField] private float transitionDuration = .8f;

        private AudioMixerSnapshot[] SnapshotArray => new[] {muffleSnapshot, clearSnapshot};

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
        }

        private void OnChangeSense(SensesState state)
        {
            if (state == SensesState.Deaf) MuffleSound();
            else UnmuffleSound();
        }

        private void UnmuffleSound()
        { TransitionSnapshot(new[] {0f, 1f}, transitionDuration); }
        private void UnmuffleSound(float duration)
        { TransitionSnapshot(new[] {0f, 1f}, duration); }

        private void MuffleSound()
        { TransitionSnapshot(new[] {1f, 0f}, transitionDuration); }
        private void MuffleSound(float duration)
        { TransitionSnapshot(new[] {1f, 0f}, duration); }

        private void TransitionSnapshot(float[] weight, float duration)
        {
            mixer.TransitionToSnapshots(SnapshotArray, weight, duration);
        }
    }
}


