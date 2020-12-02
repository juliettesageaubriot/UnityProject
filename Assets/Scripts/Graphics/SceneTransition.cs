using System;
using Global;
using UI;
using UnityEngine;

namespace Graphics
{
    [RequireComponent(typeof(UIFade))]
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField] private ScriptableSceneManager sceneManager;
        private UIFade _uiFade;

        private void Awake()
        {
            _uiFade = GetComponent<UIFade>();
        }

        private void Start()
        {
            _uiFade.FadeOut();
        }

        private void OnEnable()
        {
            sceneManager.BeforeSceneChangeEvent += _uiFade.FadeIn;
        }

        private void OnDisable()
        {
            sceneManager.BeforeSceneChangeEvent -= _uiFade.FadeIn;
        }
    }
}
