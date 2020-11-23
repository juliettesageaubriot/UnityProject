using System;
using System.Collections;
using UnityEngine;
using Utils;

namespace Interactables
{
    [Serializable]
    public struct FakeWallSwitch
    {
        public Door fakeWall;
        public float switchDelay;
    }
    
    [RequireComponent(typeof(Collider2D))]
    public class MazeWallSwitcher : MonoBehaviour
    {
        [SerializeField] private SingleUnityLayer playerLayer;
        [SerializeField] private FakeWallSwitch[] walls;

        private bool _hasTriggered;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_hasTriggered && other.gameObject.layer == playerLayer.LayerIndex)
                StartCoroutine(TriggerSwitch());
        }

        private IEnumerator TriggerSwitch()
        {
            _hasTriggered = true;
            var sequenceCopy = (FakeWallSwitch[])walls.Clone();
            Array.Sort(sequenceCopy, (m, n) => Math.Sign(m.switchDelay - n.switchDelay));
            for (var i = 0; i < sequenceCopy.Length; i++)
            {
                var timeToWait = i == 0
                    ? sequenceCopy[i].switchDelay
                    : sequenceCopy[i].switchDelay - sequenceCopy[i - 1].switchDelay;
                yield return new WaitForSeconds(timeToWait);
                var door = sequenceCopy[i].fakeWall;
                if (door.IsOpen) door.Close();
                else door.Open();
            }
        }
    }
}
