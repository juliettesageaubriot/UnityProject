using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;
using Utils;

namespace Interactables
{
	[Serializable]
	public struct DoorState
	{
		public Sprite sprite;
		public string sortingLayerName;
		public SingleUnityLayer layer;
		public bool castShadow;
		public bool collide;
	}

	public class Door : MonoBehaviour
	{
		protected Collider2D colliderComponent;
		protected SpriteRenderer spriteRenderer;
		protected ShadowCaster2D shadowCaster2D;
		
		[SerializeField] public UnityEvent onOpenEvent;
		[SerializeField] public UnityEvent onCloseEvent;

		[SerializeField] protected DoorState closedState;
		[SerializeField] protected DoorState openState;
		[SerializeField] protected bool startOpen;

		protected virtual void Start()
		{
			colliderComponent = GetComponent<Collider2D>();
			spriteRenderer = GetComponent<SpriteRenderer>();
			shadowCaster2D = GetComponent<ShadowCaster2D>();
			
			if (onOpenEvent == null)
				onOpenEvent = new UnityEvent();
			
			if (onCloseEvent == null)
				onCloseEvent = new UnityEvent();
			
			if (startOpen)
				ChangeState(openState);
			else
				ChangeState(closedState);
		}

		public void Open()
		{
			onOpenEvent.Invoke();
			ChangeState(openState);
		}

		public void Close()
		{
			onCloseEvent.Invoke();
			ChangeState(closedState);
		}

		protected void ChangeState(DoorState state)
		{
			gameObject.layer = state.layer.LayerIndex;
			colliderComponent.isTrigger = !state.collide;
			spriteRenderer.sprite = state.sprite;
			spriteRenderer.sortingLayerName = state.sortingLayerName;
			shadowCaster2D.castsShadows = state.castShadow;
		}
	}
}
