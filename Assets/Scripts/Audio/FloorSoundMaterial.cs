using UnityEngine;

namespace Audio
{
    public class FloorSoundMaterial : MonoBehaviour
    {
        [SerializeField] private SoundCollection soundCollection;
        public SoundCollection SoundCollection => soundCollection;
    }
}