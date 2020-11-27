﻿using System;
using Player;
using UI;
using UnityEngine;

namespace Camera
{
    public class SensesCam : PopupSender
    {
        [SerializeField] private PlayerSensesData data;
        [SerializeField] private UnityEngine.Camera blindCamera;
        [SerializeField] private UnityEngine.Camera normalCamera;

        private void OnEnable() { 
            data.SenseInitEvent += ChangeCams;
            data.SenseChangeEvent += UpdateCams;
        }
        private void OnDisable() { 
            data.SenseInitEvent -= ChangeCams;
            data.SenseChangeEvent -= UpdateCams;
        }

        private void UpdateCams(SensesState state)
        {
            ChangeCams(state);   
            SendPopup();
        }

        private void ChangeCams(SensesState state)
        {
            switch (state)
            {
                case SensesState.Blind:
                    ToBlind();
                    break;
                case SensesState.Deaf:
                    ToSighted();
                    break;
                case SensesState.AllSenses:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void ToBlind()
        {
            blindCamera.enabled = true;
            normalCamera.enabled = false;
        }

        private void ToSighted()
        {
            normalCamera.enabled = true;
            blindCamera.enabled = false;
        }
    
    }
}
