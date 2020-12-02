using System;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;

namespace Graphics
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraAnimator : MonoBehaviour
    {
        [SerializeField] private ScriptableCameraAnimator scriptableCameraAnimator;
        [SerializeField] private float offsetZoomStartValue;
        [SerializeField] private Vector2 offsetTravelingEndValue;
        [SerializeField] private float zoomAnimDuration;
        [SerializeField] private float travelingAnimDuration;
        [SerializeField] private Ease zoomCurve = Ease.InOutSine;
        [SerializeField] private Ease travelingCurve = Ease.InOutSine;
        private UnityEngine.Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<UnityEngine.Camera>();
            scriptableCameraAnimator.SetCameraAnimator(this);
        }

        private void Start()
        {
            ZoomIn();
        }

        public void ZoomIn()
        {
            var endZoom = _camera.orthographicSize;
            _camera.orthographicSize = endZoom + offsetZoomStartValue;
            _camera.DOOrthoSize(endZoom, zoomAnimDuration).SetEase(zoomCurve);
        }
        
        public void TravelingOut()
        {
            _camera.transform.DOMove(_camera.transform.position + (Vector3)offsetTravelingEndValue, travelingAnimDuration).SetEase(travelingCurve);
        }
    }
}
