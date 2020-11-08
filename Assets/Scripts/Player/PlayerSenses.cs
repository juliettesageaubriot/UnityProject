using System;
using System.Xml.Serialization;
using Audio;
using Global.Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
	[Serializable]
	public class SenseChangeEvent : UnityEvent<SensesState> {}

	public class PlayerSenses : MonoBehaviour
	{
		[SerializeField][TagSelector] private string fuelTag = "SenseFuel";
		[SerializeField] private PlayerSensesData data;
		[SerializeField] private PlayerInputData inputData;
		
		[SerializeField] private SensesState defaultState;
		[SerializeField] private int defaultFuelAmount;
		
		
		private void Awake()
		{
			data.InitFuel(defaultFuelAmount);
			data.InitState(defaultState);
		}

		private void OnEnable()
		{
			if (InputManager.IsReady) InputManager.ActionMaps.Player.Switch.performed += Switch;
		}

		private void OnDisable()
		{
			if (InputManager.IsReady) InputManager.ActionMaps.Player.Switch.performed -= Switch;
		}
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(fuelTag)) data.AddFuel();
		}

		private void Switch(InputAction.CallbackContext obj)
		{
			if (!inputData.Can.senseSwitch) return;
			data.Switch();
		}
	}
}
