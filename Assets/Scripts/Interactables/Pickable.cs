using UnityEngine;
using Utils;

public class Pickable : MonoBehaviour
{
    [SerializeField]
    private SingleUnityLayer playerLayer; 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer.LayerIndex)
            gameObject.SetActive(false);
    }
}
