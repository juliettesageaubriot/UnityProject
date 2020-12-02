using System;
using System.Collections.Generic;
using System.Linq;
using Global;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public class Pathfinder : MonoBehaviour
    {
        private static readonly Vector2[] Directions = {Vector2.up, Vector2.right, Vector2.down, Vector2.left};
        
        [SerializeField] private ObstacleMap obstacleMap;
        [SerializeField] private Vector2 floorOffset = Vector2.zero;
        [SerializeField] private bool autoCompute;
        [Space(10)]
        [SerializeField] private UnityEvent afterCompute;
        private GridArray<int> _distanceMap;
        public GridArray<int> DistanceMap => _distanceMap;
        
        private GridArray<bool> _computeMap;

        private void Start()
        {
            if (afterCompute == null) afterCompute = new UnityEvent();
        }
        private void OnEnable()
        { if (autoCompute) obstacleMap.OnCleanArray += ComputeDistanceMap; }
        private void OnDisable()
        { if (autoCompute) obstacleMap.OnCleanArray -= ComputeDistanceMap; }

        public void ComputeDistanceMap()
        {
            _distanceMap = GridArray<int>.From(obstacleMap.ObstacleArray);
            _distanceMap.FillArray(-1);
            _computeMap = GridArray<bool>.From(obstacleMap.ObstacleArray);
            var firstLayer = new Queue<Vector2>();
            firstLayer.Enqueue((Vector2)transform.position + floorOffset);
            ComputeLayer(firstLayer, 0, true);
            afterCompute.Invoke();
        }

        public int GetDistance(Vector2 pos)
        { return _distanceMap.Get(pos); }

        public (List<Vector2>, int) GetClosestNeigbours(Vector2 pos)
        {
            var neighbours = new List<Vector2>();
            if (_distanceMap.IsOutOfBound(pos) || _distanceMap.Get(pos) == -1) return (neighbours, -1);
            var closestDistance = 80000;
            
            foreach (var direction in Directions)
            {
                var neighbourPos = pos + direction * obstacleMap.CellSize;
                var distance = SafeGetDistance(neighbourPos);
                if (distance < closestDistance && distance != -1)
                {
                    neighbours = new List<Vector2>();
                    closestDistance = distance;
                }
                if (distance == closestDistance)
                    neighbours.Add(neighbourPos);
            }

            return (neighbours, closestDistance);
        }

        public int SafeGetDistance(Vector2 pos)
        {
            return _distanceMap.IsOutOfBound(pos) ? -1 : _distanceMap.Get(pos);
        }

        private void ComputeLayer(IEnumerable<Vector2> layerCells, int distance, bool allowObstacle = false)
        {
            var nextLayerCells = new Queue<Vector2>();
            
            var validCells = layerCells.Where(
                cell => !_computeMap.IsOutOfBound(cell)
                        && !_computeMap.Get(cell)
                        && (obstacleMap.GetObstacle(cell) != ObstacleEnum.Obstacle || allowObstacle)
            );
            
            foreach (var cell in validCells)
            {
                _distanceMap.Set(cell, distance);
                _computeMap.Set(cell, true);
                
                foreach (var direction in Directions)
                    nextLayerCells.Enqueue(cell + direction * obstacleMap.CellSize);
            }

            if (nextLayerCells.Count > 0)
                ComputeLayer(nextLayerCells, distance + 1);
        }
    }
}
