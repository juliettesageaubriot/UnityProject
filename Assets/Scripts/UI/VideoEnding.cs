using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace UI
{
    public class VideoEnding : MonoBehaviour
    {

        [SerializeField] private VideoPlayer VideoPlayer;
        [SerializeField] private UnityEvent Event;

        private void OnEnable()
        {
            VideoPlayer.loopPointReached += InvokeEvent;
        }
        
        private void OnDisable()
        {
            VideoPlayer.loopPointReached -= InvokeEvent;
        }

        private void InvokeEvent(VideoPlayer source)
        {
            Event.Invoke();
        }
        
    }
}
