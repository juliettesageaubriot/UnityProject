using System;
using System.Linq;
using Global;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerInputData", order = 10)]
    public class PlayerInputData : ScriptableObject
    {
        [SerializeField] private ScriptableSceneManager sceneManager;
        [SerializeField] private InputData[] Inputs;

        public InputData Get(string inputName)
        {
            return Inputs.First(i => i.InputName == inputName);
        }

        public bool Can(string inputName)
        {
            return Get(inputName).Can;
        }

        public void SetEnable(string inputName, bool newBool)
        {
            Get(inputName).SetEnable(newBool);
        }

        private void OnEnable()
        { sceneManager.BeforeSceneChangeEvent += EnableAll; }

        private void OnDisable()
        { sceneManager.BeforeSceneChangeEvent -= EnableAll; }

        private void EnableAll()
        {
            foreach (var inputData in Inputs) inputData.SetEnable(true);
        }
    }
}
