using System;
using Player;
using UnityEngine;

namespace Interactables
{
	public enum DoorSenseEnum
	{
		Deaf = SensesState.Deaf,
		Blind = SensesState.Blind
	}

	public class SenseDoor : Door
	{
		[SerializeField] protected PlayerSensesData data;
		[SerializeField] protected DoorSenseEnum openSenseState;
		[SerializeField] protected string playerTag;

		private bool _isPlayerOnDoor;
		private GameObject _playerGameObject;

		private void OnEnable()
		{
			data.SenseChangeEvent += OnSenseChange;
			data.SenseInitEvent += InitDoor;
		}
		private void OnDisable()
		{
			data.SenseChangeEvent -= OnSenseChange;
			data.SenseInitEvent -= InitDoor;
		}

		protected override void Start() {}

		private void InitDoor(SensesState sensesState)
		{
			startOpen = (SensesState) openSenseState == sensesState;
			base.Start();
		}
		
		private void OnSenseChange(SensesState newSense)
		{
			
			if ((SensesState)openSenseState == newSense)
				Open();
			else
			{
				Close();
				if (_isPlayerOnDoor)
					_playerGameObject.GetComponent<PlayerDeath>().Kill();
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(playerTag))
			{
				_isPlayerOnDoor = true;
				_playerGameObject = other.gameObject;
			}
		}
		private void OnTriggerExit2D(Collider2D other) { if (other.CompareTag(playerTag)) _isPlayerOnDoor = false; }
	}
}
