using UnityEngine;
using Utils;

namespace Audio
{
    public class StepSoundPlayer : RandomSoundPlayer
    {
        [SerializeField] private SingleUnityLayer floorLayer;
        private SoundCollection _defaultSounds;
    
        protected override void Start()
        {
            _defaultSounds = sounds;
            base.Start();
        }

        public void PlayStepSound(Vector2 direction)
        {
            CheckNextStepSound(direction);
            PlayRandomSound();
        }

        private void CheckNextStepSound(Vector2 direction)
        {
            var hit = Physics2D.Raycast(transform.position, direction, 1f, floorLayer.Mask);
            if (hit.collider == null)
            {
                sounds = _defaultSounds;
                return;
            }

            var floorSound = hit.collider.GetComponent<FloorSoundMaterial>();
            sounds = floorSound.SoundCollection;
        }
    }
}
