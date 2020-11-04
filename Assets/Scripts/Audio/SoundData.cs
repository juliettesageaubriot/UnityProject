using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SoundData", order = 1)]
    public class SoundData : ScriptableObject
    {
        public AudioClip[] array;

        public AudioClip GetRandomAudio()
        {
            var randomIndex = Mathf.FloorToInt(Random.Range(0f, array.Length));
            return  array[randomIndex];
        }
    }
}