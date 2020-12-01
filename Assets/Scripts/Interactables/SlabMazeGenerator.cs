using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Interactables
{
    public class SlabMazeGenerator : MonoBehaviour
    {
        [SerializeField] private Color[] colorPalette;
        [SerializeField] private GameObject hollowSlabPrefab;
        [SerializeField] private GameObject solidSlabPrefab;
        [Space(10)]
        [SerializeField] private GameObject fragileTrappedSlabPrefab;
        [SerializeField] private GameObject fragileSolidSlabPrefab;
        [Space(10)]
        [SerializeField] private Transform indicatorGroupTransform;
        [SerializeField] private Transform fragileGroupTransform;
        [SerializeField] private GameObject endPointTrigger;
        [Space(10)]
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
        private readonly Color[,] _colorArray = new Color[Width, Height];

        public void DestroyTrapSlabs()
        {
            foreach (var o in _fragileSlabsArray)
            {
                if (o.TryGetComponent<TrappedSlab>(out var trappedSlab))
                {
                    StartCoroutine(RandomDelayBreak(trappedSlab));
                }
            }
        }

        private IEnumerator RandomDelayBreak(TrappedSlab trappedSlab)
        {
            yield return new WaitForSeconds(Random.Range(0f, 0.5f));
            trappedSlab.BreakSlab();
        }
    
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
            for (var x = 0; x < Width; x++)
            for (var y = 0; y < Height; y++)
                _colorArray[x, y] = colorPalette[Random.Range(0, colorPalette.Length - 1)];
            
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

            endPointTrigger.transform.position =
                fragileGroupTransform.TransformPoint(new Vector3(lastCellX * gridSize, 3 * gridSize));
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
                slab.GetComponent<SpriteRenderer>().color = _colorArray[x, y];
                slabArray[x, y] = slab;
            }
        }
    }
}
