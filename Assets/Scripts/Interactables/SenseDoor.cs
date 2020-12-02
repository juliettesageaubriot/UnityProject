using System;
using System.Collections;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

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
		[SerializeField] protected float switchDelay = 1.8f;
		[SerializeField] protected float delayVariance = 0.2f;


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
			StartCoroutine(WaitAfterSwitch(newSense));
		}

		private IEnumerator WaitAfterSwitch(SensesState newSense)
		{
			yield return new WaitForSeconds(switchDelay + delayVariance * Random.Range(-1f, 1f));
			if ((SensesState)openSenseState == newSense)
				Open();
			else
				Close();
		}
	}
}
