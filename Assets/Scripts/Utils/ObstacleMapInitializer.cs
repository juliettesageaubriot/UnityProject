using System;
using System.Collections;
using Global;
using UnityEngine;

namespace Utils
{
    [DefaultExecutionOrder(10)]
    public class ObstacleMapInitializer : MonoBehaviour
    {
        [SerializeField] private ObstacleMap obstacleMap;
    
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private Vector2 originPoint = Vector2.zero;
        [SerializeField] private Vector2 gridSize = new Vector2(10, 10);
    
        private void Start()
        {
            obstacleMap.InitParams(originPoint, gridSize, cellSize);
            obstacleMap.CleanArray();
            StartCoroutine(WaitBeforeInit());
        }

        private IEnumerator WaitBeforeInit()
        {
            yield return new WaitForSeconds(0.1f);
            obstacleMap.CleanArray();
        }
    }
}
