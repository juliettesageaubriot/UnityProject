using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [Serializable]
    public class FuelNumberChangeEvent : UnityEvent<int> {}
    public class PlayerSenseFuel : MonoBehaviour
    {
        private int _fuelNumber = 0;
        [SerializeField] public int startFuelNumber = 3;
        [SerializeField][TagSelector] private string fuelTag = "SenseFuel";
        [SerializeField] public FuelNumberChangeEvent onFuelNumberChange;

        private void Start()
        {
            _fuelNumber = startFuelNumber;
        
            if (onFuelNumberChange == null)
                onFuelNumberChange = new FuelNumberChangeEvent();
        }

        public bool UseFuel()
        {
            if (_fuelNumber == 0) return false;
            _fuelNumber--;
            onFuelNumberChange.Invoke(_fuelNumber);
            return true;
        }

        public void AddFuel()
        {
            _fuelNumber++;
            onFuelNumberChange.Invoke(_fuelNumber);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(fuelTag)) AddFuel();
        }
    }
}