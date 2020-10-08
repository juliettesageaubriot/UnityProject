using System;
using Global.Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
	enum SensesState
	{
		AllSenses,
		Blind,
		Deaf
	}
	public class PlayerSenses : MonoBehaviour
	{
		private SensesState _state = SensesState.Deaf;

		private void OnEnable()
		{
			InputManager.ActionMaps.Player.Switch.performed += Switch;
		}

		private void OnDisable()
		{
			InputManager.ActionMaps.Player.Switch.performed -= Switch;
		}

		private void Switch(InputAction.CallbackContext context)
		{
			SensesState newState = SensesState.AllSenses;
			switch (_state)
			{
				case SensesState.Deaf:
					newState = SensesState.Blind;
					break;
				case SensesState.Blind:
					newState = SensesState.Deaf;
					break;
				case SensesState.AllSenses:
					return;
			}
			_state = newState;
		}
	}
}
