using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Audio;

public class MuffleSoundScript : MonoBehaviour
{
    public AudioMixerSnapshot inside;
    public AudioMixerSnapshot outside;

    void OnTriggerEnter(Collider other)
    {
        inside.TransitionTo(.4f);
    }

    void OnTriggerExit(Collider other)
    {
        outside.TransitionTo(.8f);
    }
}
