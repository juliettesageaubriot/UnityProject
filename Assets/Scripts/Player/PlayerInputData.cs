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
        [SerializeField] private InputData[] inputs;

        public void EnableAll()
        {
            foreach (var inputData in inputs) inputData.SetEnable(true);
        }
        public void DisableAll()
        {
            foreach (var inputData in inputs) inputData.SetEnable(false);
        }
        
        public InputData Get(string inputName)
        {
            return inputs.First(i => i.InputName == inputName);
        }

        public bool Can(string inputName)
        {
            return Get(inputName).Can;
        }

        public void SetEnable(string inputName, bool newBool)
        {
            Get(inputName).SetEnable(newBool);
        }

        public void ListenInputs()
        {
            foreach (var inputData in inputs) inputData.ListenInput();
        }

        private void OnEnable()
        { sceneManager.SceneChangeEvent += EnableAll; }

        private void OnDisable()
        { sceneManager.SceneChangeEvent -= EnableAll; }
    }
}
