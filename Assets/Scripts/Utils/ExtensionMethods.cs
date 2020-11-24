using UnityEngine;

namespace Utils
{
    public static class ExtensionMethods
    {
        public static int ManhattanDistance(this Vector2 a, Vector2 b){
            checked {
                return Mathf.Abs((int)a.x - (int)b.x) + Mathf.Abs((int)a.y - (int)b.y);
            }
        }
    }
}