using UnityEngine;

namespace Global
{
    public enum LevelsEnum
    {
        Level1,
        Level2,
        Level3
    }
    
    public class ScenesEnum : MonoBehaviour
    {
        private static string[] _levelNames = new[] {"Level 1", "Level 2", "Level 3"};
        
        public static string LevelsNames(LevelsEnum level)
        {
            return _levelNames[(int)level];
        }
    }
}
