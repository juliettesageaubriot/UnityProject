using System;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SoundData", order = 1)]
    public class SoundData : ScriptableObject
    {
        public AudioClip[] array;
    }
}