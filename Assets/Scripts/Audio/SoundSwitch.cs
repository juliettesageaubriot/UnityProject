using System.Xml.Serialization;
using Player;
using UnityEngine;

namespace Audio
{
    public class SoundSwitch : MonoBehaviour
    {
        [SerializeField] private PlayerSensesData data;
        [SerializeField] private AudioClip sound;
        [SerializeField] private AudioSource source;
        
        
        private void OnEnable()
        {
            data.SenseChangeEvent += PlaySwitchSound;
        }
        private void OnDisable()
        {
            data.SenseChangeEvent -= PlaySwitchSound;
        }
        
        private void PlaySwitchSound(SensesState newSense)
        {
            if (data.State == SensesState.Deaf) return;
            source.PlayOneShot( sound );
        }
    }
}