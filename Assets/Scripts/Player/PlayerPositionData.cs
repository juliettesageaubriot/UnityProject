using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerPositionData", menuName = "ScriptableObjects/PlayerPositionData", order = 3)]
    public class PlayerPositionData : ScriptableObject
    {
        private Transform _playerTransform;
        private Vector2 _direction = Vector2.down;
        
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
        
        public Vector2 Position => _playerTransform.position;
        public Transform Transform => _playerTransform;
        public Vector2 Direction => _direction;
    }
}