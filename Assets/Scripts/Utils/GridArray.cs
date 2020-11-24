using UnityEngine;

namespace Utils
{
    public class GridArray<T>
    {
        private float _cellSize;
        private Vector2 _originPoint ;
        private Vector2 _gridSize;

        private T[,] _array;

        public GridArray(Vector2 gridSize, Vector2 originPoint, float cellSize = 1f)
        {
            _cellSize = cellSize;
            _originPoint = originPoint;
            _gridSize = gridSize;
            _array = new T[(int)_gridSize.x, (int)_gridSize.y];
        }
        
        public void FillArray(T fillValue)
        {
            for (var i = 0; i < _gridSize.x; i++)
            for (var j = 0; j < _gridSize.y; j++)
                _array[i, j] = fillValue;
        }

        public bool IsOutOfBound(Vector2 pos)
        {
            var (x, y) = PositionToIndex(pos);
            return x < 0 || y < 0 || y > _gridSize.y - 1 || x > _gridSize.x - 1;
        }

        public T GetIndex(int x, int y)
        { return _array[x, y]; }
        public T Get(Vector2 pos)
        {
            var (x, y) = PositionToIndex(pos);
            return GetIndex(x, y);
        }

        public void SetIndex(int x, int y, T value)
        { _array[x, y] = value; }
        public void Set(Vector2 pos, T value)
        {
            var (x, y) = PositionToIndex(pos);
            SetIndex(x, y, value);
        }

        public static GridArray<T> From<TV>(GridArray<TV> fromArray)
        {
            return new GridArray<T>(fromArray._gridSize, fromArray._originPoint, fromArray._cellSize);
        }

        public (int, int) PositionToIndex(Vector2 pos)
        {
            var transformedPos = (pos - _originPoint) / _cellSize;
            return ((int)transformedPos.x, (int)transformedPos.y);
        }
    }
}