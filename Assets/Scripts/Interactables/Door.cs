﻿using System;
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
		[SerializeField] public UnityEvent<bool> onInitState;
		[Space(10)]
		[SerializeField] protected DoorState closedState;
		[SerializeField] protected DoorState openState;
		[SerializeField] protected bool startOpen;

		private bool _isOpen;
		public bool IsOpen => _isOpen;

		protected virtual void Start()
		{
			colliderComponent = GetComponent<Collider2D>();
			spriteRenderer = GetComponent<SpriteRenderer>();
			shadowCaster2D = GetComponent<ShadowCaster2D>();
			
			if (onOpenEvent == null)
				onOpenEvent = new UnityEvent();
			
			if (onCloseEvent == null)
				onCloseEvent = new UnityEvent();
			
			if (onInitState == null)
				onInitState = new UnityEvent<bool>();

			_isOpen = startOpen;
			ChangeState(startOpen ? openState : closedState);
			onInitState.Invoke(_isOpen);
		}

		public void Open()
		{
			_isOpen = true;
			ChangeState(openState);
			onOpenEvent.Invoke();
		}

		public void Close()
		{
			_isOpen = false;
			ChangeState(closedState);
			onCloseEvent.Invoke();
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
