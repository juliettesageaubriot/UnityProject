using System;
using UnityEngine;
using Utils;

namespace Audio
{
    public class StepSoundPlayer : RandomSoundPlayer
    {
        [SerializeField] private SingleUnityLayer floorLayer;
        [SerializeField] private float circleCastRadius = 0.1f;
        private int _currentColliderId;
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
            var hits = Physics2D.CircleCastAll(
                (Vector2) transform.position + direction,
                circleCastRadius,
                direction,
                0f,
                floorLayer.Mask
            );
            Collider2D hit = null;

            if (hits.Length == 1)
                hit = hits[0].collider;
            else if (hits.Length > 1)
                hit = Array.Find(hits, 
                    rHit => rHit.collider.gameObject.GetInstanceID() != _currentColliderId
                ).collider;
            
            sounds = hit == null
                ? _defaultSounds
                : hit.GetComponent<FloorSoundMaterial>().SoundCollection;

            _currentColliderId = hit == null ? 0 : hit.gameObject.GetInstanceID();
        }
    }
}
