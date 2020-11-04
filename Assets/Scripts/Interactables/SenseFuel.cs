using Audio;
using UnityEngine;

namespace Interactables
{
    public class SenseFuel : Pickable
    {
        [SerializeField] private RandomSoundPlayer soundPlayer;
        
        protected override void Pick()
        {
            soundPlayer.PlayRandomSound(() => {
                gameObject.SetActive(false);
            });
        }
    }
}
