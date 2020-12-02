using System;
using System.Collections;
using UnityEngine;

namespace Global
{
    public class SceneManagerMono : MonoBehaviour
    {
        [SerializeField] private ScriptableSceneManager sceneManager;
        [SerializeField] private float loadDelay;

        private void Awake()
        {
            sceneManager.Mono = this;
        }

        public void WaitForScene(Action callback) {WaitForScene(callback, loadDelay);}
        public void WaitForScene(Action callback, float delay)
        {
            StartCoroutine(CbCoroutine(callback, delay));
        }

        private IEnumerator CbCoroutine(Action callback, float delay)
        {
            yield return new WaitForSeconds(delay);
            callback();
        }
    }
}