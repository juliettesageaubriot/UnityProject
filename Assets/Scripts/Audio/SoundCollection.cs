using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SoundCollection", order = 1)]
    public class SoundCollection : ScriptableObject
    {
        public AudioClip[] array;

        public AudioClip GetRandomAudio()
        {
            var randomIndex = Mathf.FloorToInt(Random.Range(0f, array.Length));
            return  array[randomIndex];
        }
    }
}