using System;
using Global.Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
	public enum SensesState
	{
		AllSenses,
		Blind,
		Deaf
	}

	[Serializable]
	public class SensesSwitchEvent : UnityEvent<int, SensesState> {}

	public class PlayerSenses : MonoBehaviour
	{
		[SerializeField] private SensesState defaultState;
		[SerializeField] private int maxSwitchCount = 3;
		[SerializeField] private Camera mainCamera;
		[SerializeField] private Camera blindCamera;
		[SerializeField] private AudioListener audioListener;
		[SerializeField] private SensesSwitchEvent onSensesSwitch;

		private int _switchCount = 0;

		private SensesState _state;
		private SensesState State {
			get => _state;
			set
			{
				switch (value)
				{
					case SensesState.Blind:
						ToBlind(_state);
						break;
					case SensesState.Deaf:
						ToDeaf(_state);
						break;
					case SensesState.AllSenses:
						ToAllSenses(_state);
						break;
				}
				_state = value;
			}
		}

		private void Start()
		{
			if (onSensesSwitch == null)
				onSensesSwitch = new SensesSwitchEvent();

			State = defaultState;
		}

		private void OnEnable()
		{
			if (InputManager.IsReady) InputManager.ActionMaps.Player.Switch.performed += Switch;
		}

		private void OnDisable()
		{
			if (InputManager.IsReady) InputManager.ActionMaps.Player.Switch.performed -= Switch;
		}

		private void Switch(InputAction.CallbackContext context)
		{
			if(_switchCount >= maxSwitchCount) return;
			SensesState newState = SensesState.AllSenses;
			switch (State)
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

			_switchCount++;
			State = newState;
			onSensesSwitch.Invoke(_switchCount, State);
		}

		private void ToBlind(SensesState oldValue)
		{
			mainCamera.enabled = false;
			blindCamera.enabled = true;
			audioListener.enabled = false;
		}

		private void ToDeaf(SensesState oldValue)
		{
			mainCamera.enabled = true;
			blindCamera.enabled = false;
			audioListener.enabled = true;
		}

		private void ToAllSenses(SensesState oldValue)
		{
			mainCamera.enabled = true;
			blindCamera.enabled = false;
			audioListener.enabled = true;
		}

	}
}
