using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerPositionData", menuName = "ScriptableObjects/PlayerPositionData", order = 3)]
    public class PlayerPositionData : ScriptableObject
    {
        [SerializeField] private Vector2 center; 
        private Transform _playerTransform;
        private Vector2 _direction = Vector2.down;
        private bool _isMoving;
        public bool IsMoving => _isMoving;
        
        public delegate void DirectionChangeHandler(Vector2 direction);
        public event DirectionChangeHandler DirectionChangeEvent;

        public void InitTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        public void UpdateDirection(Vector2 direction)
        {
            _direction = direction;
            DirectionChangeEvent?.Invoke(_direction);
        }

        public void UpdateIsMoving(bool move)
        {
            _isMoving = move;
        }
        
        public Vector2 Position => _playerTransform.position;
        public Vector2 CenterPosition => Position + center;
        public Transform Transform => _playerTransform;
        public Vector2 Direction => _direction;
    }
}