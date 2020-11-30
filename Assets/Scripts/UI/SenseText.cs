using System;
using Player;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(UICharacterDelay))]
    public class SenseText : MonoBehaviour
    {
        private UICharacterDelay _characterDelay;
        [SerializeField] private PlayerSensesData sensesData;
        [SerializeField] private bool setTextAtStart = true;
        [SerializeField] [TextArea(1, 10)] private string deafText;
        [SerializeField] [TextArea(1, 10)] private string blindText;
        [SerializeField] [TextArea(1, 10)] private string allSenseText;

        private void Start()
        {
            _characterDelay = GetComponent<UICharacterDelay>();
            if (setTextAtStart) SetText();
        }

        public void SetText()
        {
            switch (sensesData.State)
            {
                case SensesState.Blind:
                    _characterDelay.SetText(blindText);
                    break;
                case SensesState.Deaf:
                    _characterDelay.SetText(deafText);
                    break;
                case SensesState.AllSenses:
                    _characterDelay.SetText(allSenseText);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
