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
		[Space(10)]
		[SerializeField] protected PlayerSensesData data;
		[SerializeField] protected DoorSenseEnum openSenseState;


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

		private void InitDoor(SensesState sensesState)
		{
			isOpen = (SensesState) openSenseState == sensesState;
			InitState();
		}
		
		private void OnSenseChange(SensesState newSense)
		{
			
			if ((SensesState)openSenseState == newSense)
				Open();
			else
				Close();
		}
	}
}
