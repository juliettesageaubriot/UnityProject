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

		private void OnEnable() { data.SenseChangeEvent += OnSenseChange; }
		private void OnDisable() { data.SenseChangeEvent -= OnSenseChange; }

		protected override void Start()
		{
			startOpen = (SensesState) openSenseState == data.State;
			base.Start();
		}
		
		public void OnSenseChange(SensesState newSense)
		{
			if ((SensesState)openSenseState == newSense)
				Open();
			else
				Close();
		}
	}
}
