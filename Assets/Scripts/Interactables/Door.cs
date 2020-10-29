﻿using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Interactables
{
	[Serializable]
	public struct DoorState
	{
		public Sprite sprite;
		public string sortingLayerName;
		public bool castShadow;
		public bool collide;
	}

	public class Door : MonoBehaviour
	{
		protected Collider2D colliderComponent;
		protected SpriteRenderer spriteRenderer;
		protected ShadowCaster2D shadowCaster2D;

		[SerializeField] protected DoorState closedState;
		[SerializeField] protected DoorState openState;
		[SerializeField] protected bool startOpen;

		protected virtual void Start()
		{
			colliderComponent = GetComponent<Collider2D>();
			spriteRenderer = GetComponent<SpriteRenderer>();
			shadowCaster2D = GetComponent<ShadowCaster2D>();
			if (startOpen)
				Open();
			else
				Close();
		}

		public void Open()
		{
			ChangeState(openState);
		}

		public void Close()
		{
			ChangeState(closedState);
		}

		protected void ChangeState(DoorState state)
		{
			colliderComponent.enabled = state.collide;
			spriteRenderer.sprite = state.sprite;
			spriteRenderer.sortingLayerName = state.sortingLayerName;
			shadowCaster2D.castsShadows = state.castShadow;
		}
	}
}
