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

		public void OnSenseChange(SensesState newSense)
		{
			if ((SensesState)openSenseState == newSense)
				Open();
			else
				Close();
		}
	}
}
