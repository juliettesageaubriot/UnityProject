using UnityEngine;
using UnityEngine.Events;

namespace Global
{
    public class OnStartEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent onStart;
        void Start()
        {
            onStart.Invoke();
        }
    
    }
}
