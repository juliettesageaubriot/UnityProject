using UnityEngine;
using UnityEngine.Events;

namespace Interactables
{
	public class PressurePlate : MonoBehaviour
	{
		[SerializeField] private LayerMask colliderLayers;
		[SerializeField] private Sprite upSprite;
		[SerializeField] private Sprite downSprite;
		[SerializeField] private UnityEvent onPlateEnter;
		[SerializeField] private UnityEvent onPlateExit;

		private SpriteRenderer _spriteRenderer;

		void Start()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			if (onPlateEnter == null) onPlateEnter = new UnityEvent();
			if (onPlateExit == null) onPlateExit = new UnityEvent();
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (colliderLayers.value == (colliderLayers.value | (1 << other.gameObject.layer)))
			{
				_spriteRenderer.sprite = downSprite;
				onPlateEnter.Invoke();
			}
		}

		void OnTriggerExit2D(Collider2D other)
		{
			if (colliderLayers.value == (colliderLayers.value | (1 << other.gameObject.layer)))
			{
				_spriteRenderer.sprite = upSprite;
				onPlateExit.Invoke();
			}
		}
	}
}
