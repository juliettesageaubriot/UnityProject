using UnityEngine;
using Utils;

namespace UI
{
    public class SlabTuto : MonoBehaviour
    {
        [SerializeField] private ScriptableResetCounter scriptableResetCounter;
        
        private EventSequence _sequence;
        void Start()
        {
            _sequence = GetComponent<EventSequence>();
            if (scriptableResetCounter.ResetCounter > 4 && scriptableResetCounter.ResetCounter < 8)
            {
                _sequence.StartSequence();
            }
        }
    }
}
