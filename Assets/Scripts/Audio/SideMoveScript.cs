using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMoveScript : MonoBehaviour
{

    [Range(0,10)]
    public float Amount;
    private Vector3 _initPosition;
    void Start()
    {
        _initPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
    }
    
    
    void Update()
    {
        Vector3 pos = _initPosition;
        pos.z = _initPosition.z + Mathf.Sin(Time.time) * Amount;
        transform.position = pos;
    }
}
