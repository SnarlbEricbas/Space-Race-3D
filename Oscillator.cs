using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movement = new Vector3(10f,10f,10f);
    [Range(0, 1)] [SerializeField] float factor;
    [SerializeField] float period = 2f;

    Vector3 startPos;
    void Start()
    {
        startPos = transform.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //set factor value automatically

        float cycles = Time.time / period; //0---
        float rawSineWave = Mathf.Sin(2 * Mathf.PI * cycles);

        factor = rawSineWave / 2f + 0.5f;

        Vector3 offset = movement * factor;
        transform.position = startPos + offset;
        
        
    }
}
