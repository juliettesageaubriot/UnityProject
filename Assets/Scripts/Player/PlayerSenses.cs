using System;
using System.Xml.Serialization;
using Audio;
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
	public class SenseChangeEvent : UnityEvent<SensesState> {}

	public class PlayerSenses : MonoBehaviour
	{
		[SerializeField] public SensesState defaultState;
		[SerializeField] private Camera mainCamera;
		[SerializeField] private Camera blindCamera;
		[SerializeField] private SenseChangeEvent onSenseChange;
		
		private SnapshotManager _snapshotManager;
		private PlayerSenseFuel _fuel;

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
			if (onSenseChange == null)
				onSenseChange = new SenseChangeEvent();
			
			if(SnapshotManager.IsReady) _snapshotManager = SnapshotManager.Instance;
			State = defaultState;
			_fuel = GetComponent<PlayerSenseFuel>();
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
			if(!_fuel.UseFuel()) return;
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

			State = newState;
			onSenseChange.Invoke(State);
		}

		private void ToBlind(SensesState oldValue)
		{
			mainCamera.enabled = false;
			blindCamera.enabled = true;
			_snapshotManager.UnmuffleSound();
		}

		private void ToDeaf(SensesState oldValue)
		{
			mainCamera.enabled = true;
			blindCamera.enabled = false;
			_snapshotManager.MuffleSound();
		}

		private void ToAllSenses(SensesState oldValue)
		{
			mainCamera.enabled = true;
			blindCamera.enabled = false;
			_snapshotManager.UnmuffleSound();
		}
	}
}
