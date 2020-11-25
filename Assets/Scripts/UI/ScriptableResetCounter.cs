using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    [CreateAssetMenu(fileName = "ResetCounter", menuName = "ScriptableObjects/ScriptableResetCounter", order = 3)] 
    public class ScriptableResetCounter : ScriptableObject
    {
        public int GlobalResetCounter => _sceneResetCounter.Values.Sum();
        public int ResetCounter => _sceneResetCounter[SceneManager.GetActiveScene().name];

        private Dictionary<string, int> _sceneResetCounter;
        
        public void Init()
        {
            _sceneResetCounter =
                new Dictionary<string, int>();
        }
        
        public void OnReset()
        {
            int currentCount;
            string id = SceneManager.GetActiveScene().name;
            
            _sceneResetCounter.TryGetValue(id, out currentCount);
            _sceneResetCounter[id] = currentCount + 1;
        }

    }
}