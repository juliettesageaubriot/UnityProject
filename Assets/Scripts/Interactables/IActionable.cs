using UnityEngine;

namespace Interactables
{
    public interface IActionable
    {
        void Action();
        bool IsActionable();
    }
}