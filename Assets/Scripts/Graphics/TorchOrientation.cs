using System;
using UnityEngine;

namespace Graphics
{
    public enum TorchOrientationEnum
    {
        Right,
        Left,
        Front
    }
    
    public class TorchOrientation : MonoBehaviour
    {
        private static readonly int Orientation = Animator.StringToHash("Orientation");
        
        [SerializeField] private TorchOrientationEnum torchDirection;

        private void Start()
        {
            var orientation = 0.5f;
            switch (torchDirection)
            {
                case TorchOrientationEnum.Front:
                    orientation = 0.5f;
                    break;
                case TorchOrientationEnum.Right:
                    orientation = 1f;
                    break;
                case TorchOrientationEnum.Left:
                    orientation = 0f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            GetComponent<Animator>().SetFloat(Orientation, orientation);
        }
    }
}
