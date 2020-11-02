using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class SnapshotManager : MonoBehaviour

{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioMixerSnapshot[] snapshots;
    
    [SerializeField] public UnityEvent onPlaySound;
    [SerializeField] public UnityEvent onStopSound;

    public float[] weights;
    
    private void Start()
    {
        if (onPlaySound == null)
            onPlaySound = new UnityEvent();
			
        if (onStopSound == null)
            onStopSound = new UnityEvent();
    }

    public void Play()
    {
        Debug.Log("play");
        weights[0] = 1f;
        weights[1] = 0f;
        mixer.TransitionToSnapshots(snapshots, weights, .2f);
        onPlaySound.Invoke();
    }
    
    public void Stop()
    {
        Debug.Log("stop");
        weights[0] = 0f;
        weights[1] = 1f;
        mixer.TransitionToSnapshots(snapshots, weights, .2f);
        onStopSound.Invoke();

    }
}

