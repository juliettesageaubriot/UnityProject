using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Interactables
{
    public class TrappedSlab : PlayerKiller
    {
        [SerializeField] private UnityEvent onSlabBreak;

        private void Start()
        {
            if (onSlabBreak == null)
                onSlabBreak = new UnityEvent();
        }

        protected override void OnKill()
        {
            BreakSlab();
        }

        public void BreakSlab()
        {
            onSlabBreak.Invoke();
        }
        
        
    }
}
