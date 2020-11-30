using System.Collections;
using Audio;
using Player;
using UnityEngine;

namespace Interactables
{
    public class SenseFuel : Pickable, IActionable
    {
        [SerializeField] private RandomSoundPlayer soundPlayer;
        [SerializeField] private PlayerSensesData sensesData;
        
        protected override void Pick()
        {
            StartCoroutine(LatePick());
            soundPlayer.PlayRandomSound(() => {
                gameObject.SetActive(false);
            });
            sensesData.AddFuel();
        }

        public void Action()
        {
            Pick();
        }

        public bool IsActionable()
        {
            return true;
        }

        private IEnumerator LatePick()
        {
            yield return new WaitForSeconds(0.1f);
            base.Pick();
        }
    }
}
