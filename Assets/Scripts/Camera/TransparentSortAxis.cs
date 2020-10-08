using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentSortAxis : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
	    var cam = GetComponent<Camera>();
	    cam.transparencySortMode = TransparencySortMode.CustomAxis;
	    cam.transparencySortAxis = new Vector3(0, 1, 0);
    }
}
