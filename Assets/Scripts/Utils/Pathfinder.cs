using System.Collections.Generic;
using System.Linq;
using Global;
using UnityEngine;

namespace Utils
{
    public class Pathfinder : MonoBehaviour
    {
        private static readonly Vector2[] Directions = {Vector2.up, Vector2.right, Vector2.down, Vector2.left};
        
        [SerializeField] private ObstacleMap obstacleMap;
        [SerializeField] private bool autoCompute;
        private GridArray<int> _distanceMap;
        private GridArray<bool> _computeMap;

        private void Start()
        { if (autoCompute) ComputeDistanceMap(); }
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
            firstLayer.Enqueue(transform.position);
            ComputeLayer(firstLayer, 0);
        }

        public int GetDistance(Vector2 pos)
        { return _distanceMap.Get(pos); }

        private void ComputeLayer(IEnumerable<Vector2> layerCells, int distance)
        {
            var nextLayerCells = new Queue<Vector2>();
            
            var validCells = layerCells.Where(
                cell => !_computeMap.IsOutOfBound(cell)
                        && !_computeMap.Get(cell)
                        && obstacleMap.GetObstacle(cell) != ObstacleEnum.Obstacle
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
