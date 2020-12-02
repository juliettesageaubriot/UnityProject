using System;
using Global;
using Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;
using Utils;

namespace Interactables
{
	[Serializable]

	public class Door : MonoBehaviour
	{

		private bool _isPlayerOnDoor;
		private GameObject _playerGameObject;
		
		[SerializeField] public UnityEvent onOpenEvent;
		[SerializeField] public UnityEvent onCloseEvent;
		[SerializeField] public UnityEvent<bool> onInitState;
		[Space(10)]
		[SerializeField] protected bool startOpen;
		[SerializeField] protected string playerTag;
		[Space(10)]
		[SerializeField] protected SingleUnityLayer openLayer;
		[SerializeField] protected SingleUnityLayer closedLayer;
		[SerializeField] protected string openSortingLayer;
		[SerializeField] protected string closedSortingLayer;
		[Space(10)]
		[SerializeField] protected DoorAnimator doorAnimator;
		[SerializeField] protected ShadowCaster2D shadowCaster;
		[SerializeField] protected ObstacleMap obstacleMap;
		[SerializeField] protected SpriteRenderer doorFloor;
		
		protected bool isOpen;
		public bool IsOpen => isOpen;

		protected virtual void Start()
		{
			
			if (onOpenEvent == null)
				onOpenEvent = new UnityEvent();
			
			if (onCloseEvent == null)
				onCloseEvent = new UnityEvent();
			
			if (onInitState == null)
				onInitState = new UnityEvent<bool>();

			isOpen = startOpen;
		}

		protected void InitState()
		{
			onInitState.Invoke(isOpen);
			if (isOpen) doorAnimator.OpenImmediate();
			else doorAnimator.CloseImmediate();
			shadowCaster.castsShadows = !isOpen;
			gameObject.layer = isOpen ? openLayer.LayerIndex : closedLayer.LayerIndex;
			doorFloor.sortingLayerName = isOpen ? openSortingLayer : closedSortingLayer;
		}

		public void Open()
		{
			isOpen = true;
			shadowCaster.castsShadows = !isOpen;
			doorAnimator.Open();
			gameObject.layer = openLayer.LayerIndex;
			doorFloor.sortingLayerName = openSortingLayer;
			
			obstacleMap.CleanArray();

			onOpenEvent.Invoke();
		}

		public void Close()
		{
			isOpen = false;
			shadowCaster.castsShadows = !isOpen;
			doorAnimator.Close();
			gameObject.layer = closedLayer.LayerIndex;
			doorFloor.sortingLayerName = closedSortingLayer;
			
			obstacleMap.CleanArray();

			onCloseEvent.Invoke();
			if (_isPlayerOnDoor)
				_playerGameObject.GetComponent<PlayerDeath>().Kill();
		}

		private void OnTriggerExit2D(Collider2D other) { if (other.CompareTag(playerTag)) _isPlayerOnDoor = false; }
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(playerTag))
			{
				_isPlayerOnDoor = true;
				_playerGameObject = other.gameObject;
			}
		}
	}
}
