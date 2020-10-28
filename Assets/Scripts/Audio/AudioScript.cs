using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{

    private AudioSource _source;
    void Start()
    {
        _source = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !_source.isPlaying)
        {
            _source.Play();
        }
    }
}
