using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    [Serializable]
    public struct EventInTime
    {
        public float interval;
        public UnityEvent unityEvent;
    }

    public class EventSequence : MonoBehaviour
    {
        [SerializeField] private bool playOnAwake;
        [SerializeField] private bool relativeToPrevious = true;
        [SerializeField] private EventInTime[] sequence;

        private Coroutine _coroutine;
        private bool _isRunning;

        public void Awake()
        {
            if (playOnAwake) StartSequence();
        }

        public void StartSequence()
        {
            StopSequence();
            _coroutine = StartCoroutine(SequenceCoroutine());
        }

        public void StopSequence()
        {
            if (_isRunning && _coroutine != null) StopCoroutine(_coroutine);
            _isRunning = false;
        }

        private IEnumerator SequenceCoroutine()
        {
            _isRunning = true;
            var sequenceCopy = (EventInTime[])sequence.Clone();
            if (!relativeToPrevious)
                Array.Sort(sequenceCopy, (m, n) => Math.Sign(m.interval - n.interval));
            for (var i = 0; i < sequenceCopy.Length; i++)
            {
                var timeToWait = i == 0 || relativeToPrevious
                    ? sequenceCopy[i].interval
                    : sequenceCopy[i].interval - sequenceCopy[i - 1].interval;
                yield return new WaitForSeconds(timeToWait);
                sequenceCopy[i].unityEvent.Invoke();
            }

            _isRunning = false;
        }
    }
}