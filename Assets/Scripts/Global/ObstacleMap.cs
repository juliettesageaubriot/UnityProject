using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Global
{
    public enum ObstacleEnum
    {
        Obstacle,
        NotObstacle,
        Unknown
    }
    
    [CreateAssetMenu(fileName = "ObstacleMap", menuName = "ScriptableObjects/ObstacleMap", order = 2)]
    public class ObstacleMap : ScriptableObject
    {
        private GridArray<ObstacleEnum> _obstacleArray;
        public GridArray<ObstacleEnum> ObstacleArray => _obstacleArray;
        
        [SerializeField] private float cellSize = 1f;
        public float CellSize => cellSize;
        
        [SerializeField] private Vector2 originPoint = Vector2.zero;
        public Vector2 OriginPoint => originPoint;
        
        [SerializeField] private Vector2 gridSize = new Vector2(10, 10);
        public Vector2 GridSize => gridSize;
        
        [SerializeField] private SingleUnityLayer obstacleLayer;
        [SerializeField] private Vector2 raycastOffset = Vector2.zero;
        
        public delegate void CleanArrayHandler();
        public event CleanArrayHandler OnCleanArray;

        public void InitParams(Vector2 newOriginPoint, Vector2 newGridSize, float newCellSize)
        {
            originPoint = newOriginPoint;
            gridSize = newGridSize;
            cellSize = newCellSize;
        }
        
        public void CleanArray()
        {
            InitArray();
            OnCleanArray?.Invoke();
        }

        public ObstacleEnum GetObstacle(Vector2 position, bool getUnknown = false)
        {
            var obstacle = _obstacleArray.Get(position);

            if (getUnknown || obstacle != ObstacleEnum.Unknown) return obstacle;
            var hit = Physics2D.CircleCast(
                position + raycastOffset,
                0.1f,
                Vector2.zero,
                0f,
                obstacleLayer.Mask);
            
            
            var newObstacle = hit.collider == null ? ObstacleEnum.NotObstacle : ObstacleEnum.Obstacle;
            _obstacleArray.Set(position, newObstacle);
            return newObstacle;
        }

        private void InitArray()
        {
            _obstacleArray = new GridArray<ObstacleEnum>(gridSize, originPoint, cellSize);
            _obstacleArray.FillArray(ObstacleEnum.Unknown);
        }
    }
}
