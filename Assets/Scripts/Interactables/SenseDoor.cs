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
		[SerializeField] protected DoorSenseEnum openSenseState;

		protected override void Start()
		{
			var playerSense = PlayerAccess.currentPlayer.GetComponent<PlayerSenses>();
			startOpen = (SensesState) openSenseState == playerSense.defaultState;
			playerSense.onSenseChange.AddListener(OnSenseChange);
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
