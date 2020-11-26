using System;
using System.Xml.Serialization;
using Audio;
using Global;
using Global.Input;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utils;

namespace Player
{
	[Serializable]
	public class SenseChangeEvent : UnityEvent<SensesState> {}

	[DefaultExecutionOrder(100)]
	public class PlayerSenses : MonoBehaviour
	{
		[SerializeField][TagSelector] private string fuelTag = "SenseFuel";
		[SerializeField] private PlayerSensesData data;
		[SerializeField] private InputData switchInput;

		[SerializeField] private SensesState defaultState;
		[SerializeField] private int defaultFuelAmount;
		
		private void Start()
		{
			data.InitFuel(defaultFuelAmount);
			data.InitState(defaultState);
		}

		private void OnEnable()
		{
			switchInput.AddListener(Switch);
		}

		private void OnDisable()
		{
			switchInput.RemoveListener(Switch);
		}
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(fuelTag)) data.AddFuel();
		}

		private void Switch(InputAction.CallbackContext obj)
		{
			data.Switch();
		}
	}
}
