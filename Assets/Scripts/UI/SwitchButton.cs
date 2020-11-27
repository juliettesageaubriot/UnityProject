using Global;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SwitchButton : InputButton
    {
        [SerializeField] private PlayerSensesData sensesData;


        protected override void OnEnable()
        {
            sensesData.FuelChangeEvent += UpdateButtonInteractable;
            sensesData.FuelInitEvent += InitButtonInteractable;
            base.OnEnable();
        }

        protected override void OnDisable()
        { 
            sensesData.FuelChangeEvent -= UpdateButtonInteractable;
            sensesData.FuelInitEvent -= InitButtonInteractable;
            base.OnDisable();
        }

        private void UpdateButtonInteractable(int fuelAmount)
        {
            UpdateButtonInteractable(fuelAmount, inputData.Can, fade.FadeDuration);
        }
        
        protected override void UpdateButtonInteractable(bool isInputEnable, float duration)
        {
            UpdateButtonInteractable(sensesData.FuelAmount, isInputEnable, duration);
        }

        private void InitButtonInteractable(int fuelAmount)
        {
            UpdateButtonInteractable(fuelAmount, inputData.Can, 0f);
        }

        private void UpdateButtonInteractable(int fuelAmount, bool isInputEnable, float duration)
        {
            var isNowInteractable = fuelAmount != 0 && isInputEnable;
            if (isNowInteractable != button.interactable)
            {
                if (isNowInteractable) fade.FadeIn(duration);
                else fade.FadeOut(duration);
            }
            button.interactable = isNowInteractable;
        }
    }
}
