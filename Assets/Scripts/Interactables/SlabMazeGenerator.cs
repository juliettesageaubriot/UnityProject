using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Interactables
{
    public class SlabMazeGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject hollowSlabPrefab;
        [SerializeField] private GameObject solidSlabPrefab;
    
        [SerializeField] private GameObject fragileTrappedSlabPrefab;
        [SerializeField] private GameObject fragileSolidSlabPrefab;

        [SerializeField] private Transform indicatorGroupTransform;
        [SerializeField] private Transform fragileGroupTransform;
    
        [SerializeField] private float gridSize = 1f;
    

        // Currently doesn't support a different height
        private const int Width = 5;
        private const int Height = 3;
    
        /**
         * y ^
         *   |
         *   |
         *   |
         * 0 +-------- >
         *   0         x
         * 
         *  _layoutArray[x, y]
         *
         *  true: solid slab
         *  false: hollow slab
         */
        private readonly bool[,] _layoutArray = new bool[Width, Height];
        private readonly GameObject[,] _fragileSlabsArray = new GameObject[Width, Height];
        private readonly GameObject[,] _indicatorSlabsArray = new GameObject[Width, Height];
    
    
        private void Start()
        {
            GenerateLayout();
        
            InstantiateSlabs(
                fragileGroupTransform,
                _fragileSlabsArray,
                fragileSolidSlabPrefab,
                fragileTrappedSlabPrefab
            );
        
            InstantiateSlabs(
                indicatorGroupTransform,
                _indicatorSlabsArray,
                solidSlabPrefab,
                hollowSlabPrefab
            );
        }

        private void GenerateLayout()
        {
            // Fill first row
            var startCellX = Random.Range(0, Width);
            for (var i = 0; i < Width; i++)
                _layoutArray[i, 0] = i == startCellX;
        
            // Fill last row
            var lastCellX = Random.Range(0, Width);
            while (lastCellX == startCellX)
                lastCellX = Random.Range(0, Width);

            for (var i = 0; i < Width; i++)
                _layoutArray[i, Height - 1] = i == lastCellX;
        
            // Fill middle row
            for (var i = 0; i < Width; i++)
                _layoutArray[i, 1] = i >= Math.Min(startCellX, lastCellX) && i <= Math.Max(startCellX, lastCellX);

        }

        private void InstantiateSlabs(
            Transform groupTransform,
            GameObject[,] slabArray,
            GameObject solidSlab,
            GameObject hollowSlab
        ) {
            for (var x = 0; x < Width; x++)
            for (var y = 0; y < Height; y++)
            {
                var slab = Instantiate(
                    _layoutArray[x, y] ? solidSlab : hollowSlab,
                    groupTransform.TransformPoint(
                        new Vector3(x * gridSize, y * gridSize)
                    ),
                    Quaternion.identity,
                    groupTransform
                );
                slab.AddComponent(typeof(ScaleOnAwake));
                slabArray[x, y] = slab;
            }
        }
    }
}
