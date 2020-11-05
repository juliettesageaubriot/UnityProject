using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class SensesCam : MonoBehaviour
{
    [SerializeField] private PlayerSensesData data;
    [SerializeField] private Camera blindCamera;
    [SerializeField] private Camera normalCamera;

    private void Start()
    {
        UpdateCams(data.State);
    }

    private void OnEnable() { data.SenseChangeEvent += UpdateCams; }
    private void OnDisable() { data.SenseChangeEvent -= UpdateCams; }

    private void UpdateCams(SensesState state)
    {
        blindCamera.enabled = state == SensesState.Blind;
        normalCamera.enabled = state != SensesState.Blind;
    }
}
