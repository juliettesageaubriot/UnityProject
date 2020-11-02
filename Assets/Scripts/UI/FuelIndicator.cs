using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FuelIndicator : MonoBehaviour
    {
        [SerializeField] private GameObject fuelIconPrefab;
        private readonly Stack<GameObject> _fuelIcons = new Stack<GameObject>();
    
        void Start()
        {
            var playerSenseFuel = PlayerAccess.currentPlayer.GetComponent<PlayerSenseFuel>();
            UpdateIcons(playerSenseFuel.startFuelNumber);
            playerSenseFuel.onFuelNumberChange.AddListener(UpdateIcons);
        }

        private void UpdateIcons(int newAmount)
        {
            var fuelCount = _fuelIcons.Count;
            if (newAmount == fuelCount) return;
        
            if (newAmount > fuelCount)
                // Add several
                for (var i = 0; i < newAmount - fuelCount; i++)
                    AddOneIcon();
            else
                // Remove several
                for (var i = 0; i < fuelCount - newAmount; i++)
                    RemoveOneIcon();
        
            // Rebuild layout to force horizontal layout group to do his job
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
        }

        private void AddOneIcon()
        {
            _fuelIcons.Push(Instantiate(fuelIconPrefab, transform));
        }

        private void RemoveOneIcon()
        {
            Destroy(_fuelIcons.Pop());
        }
    }
}
