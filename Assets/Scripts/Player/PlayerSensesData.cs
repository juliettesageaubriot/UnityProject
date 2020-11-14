using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public enum SensesState
    {
        AllSenses,
        Blind,
        Deaf
    }
    
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerSensesData", order = 11)]
    public class PlayerSensesData : ScriptableObject
    {
        public delegate void FuelChangeHandler(int fuelAmount);
        public event FuelChangeHandler FuelChangeEvent;
        public event FuelChangeHandler FuelInitEvent;
        
        public delegate void SenseChangeHandler(SensesState state);
        public event SenseChangeHandler SenseChangeEvent;
        public event SenseChangeHandler SenseInitEvent;
        
        private int _fuelAmount = 0;
        public int FuelAmount {
            get => _fuelAmount;
            private set
            {
                if (_fuelAmount != value)
                    FuelChangeEvent?.Invoke(value);
                _fuelAmount = value;
            }
        }

        private SensesState _state = SensesState.AllSenses;
        public SensesState State {
            get => _state;
            private set
            {
                if (_state != value)
                    SenseChangeEvent?.Invoke(value);
                _state = value;
            }
        }

        private bool UseFuel()
        {
            if (FuelAmount == 0) return false;
            FuelAmount--;
            return true;
        }

        public void AddFuel()
        {
            FuelAmount++;
        }

        public void Switch()
        {
            if(SensesState.AllSenses == State || !UseFuel()) return;
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
        }
        
        public void InitState(SensesState state)
        {
            _state = state;
            SenseInitEvent?.Invoke(state);
        }
        
        public void InitFuel(int fuelAmount)
        {
            _fuelAmount = fuelAmount;
            FuelInitEvent?.Invoke(fuelAmount);
        }

    }
}
