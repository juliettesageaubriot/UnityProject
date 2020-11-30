using System;
using Player;
using UnityEngine;

namespace UI.Popup
{
    public class FuelObtainedPopup : PopupSequence
    {
        [SerializeField] private PlayerSensesData playerSensesData;

        private void OnEnable()
        {
            playerSensesData.SenseChangeEvent += HandleSwitch;
        }
        private void OnDisable()
        {
            playerSensesData.SenseChangeEvent -= HandleSwitch;
        }

        private void HandleSwitch(SensesState state)
        {
            DestroyPopup();
        }
    }
}
