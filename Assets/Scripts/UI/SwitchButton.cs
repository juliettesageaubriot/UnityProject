using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SwitchButton : MonoBehaviour
    {
        [SerializeField] private PlayerSensesData sensesData;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        { sensesData.FuelChangeEvent += OnFuelChange; }
        private void OnDisable()
        { sensesData.FuelChangeEvent -= OnFuelChange; }

        private void OnFuelChange(int fuelAmount)
        {
            _button.interactable = fuelAmount != 0;
        }
    }
}
