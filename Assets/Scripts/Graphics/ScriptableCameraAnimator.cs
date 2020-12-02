using JetBrains.Annotations;
using UnityEngine;

namespace Graphics
{
    
    [CreateAssetMenu(fileName = "ScriptableCameraAnimator", menuName = "ScriptableObjects/ScriptableCameraAnimator", order = 100)]
    public class ScriptableCameraAnimator : ScriptableObject
    {
        [CanBeNull] private CameraAnimator _cameraAnimator;

        public void SetCameraAnimator(CameraAnimator cameraAnimator)
        {
            _cameraAnimator = cameraAnimator;
        }

        public void ZoomIn()
        {
            if (_cameraAnimator != null) _cameraAnimator.ZoomIn();
        }

        public void TravelingOut()
        {
            if (_cameraAnimator != null) _cameraAnimator.TravelingOut();
        }
    }
}