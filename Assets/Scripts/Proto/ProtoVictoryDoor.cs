using UnityEngine;
using UnityEngine.Events;

namespace Proto
{
    public class ProtoVictoryDoor : MonoBehaviour
    {
        [SerializeField] private UnityEvent onTriggerEnter;

        private void Start()
        {
            if (onTriggerEnter == null)
                onTriggerEnter = new UnityEvent();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            onTriggerEnter.Invoke();
        }
    }
}
