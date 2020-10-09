using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        audio.volume -= Time.deltaTime * 2;
        audio.spatialBlend -= Time.deltaTime * 2;
        Debug.Log(audio.volume);
    }
}
