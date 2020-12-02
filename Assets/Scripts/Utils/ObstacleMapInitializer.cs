using System.Collections;
using Global;
using UnityEngine;

namespace Utils
{
    public class ObstacleMapInitializer : MonoBehaviour
    {
        [SerializeField] private ObstacleMap obstacleMap;
    
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private Vector2 originPoint = Vector2.zero;
        [SerializeField] private Vector2 gridSize = new Vector2(10, 10);
    
        void Awake()
        {
            obstacleMap.InitParams(originPoint, gridSize, cellSize);
            StartCoroutine(WaitBeforeInit());
        }

        private IEnumerator WaitBeforeInit()
        {
            yield return new WaitForFixedUpdate();
            obstacleMap.CleanArray();
        }
    }
}
