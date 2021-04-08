using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 movementVector;
    [SerializeField] [Range(0, 1)] private float movementFactor;
    [SerializeField] [Range(0.1f, 2f)] private float period = 2f;
    private Vector3 _startingPosition;

    void Start()
    {
        _startingPosition = transform.position;
    }

    void Update()
    {
        const float tau = Mathf.PI * 2;
        var cycles = Time.time / period; // continually growing
        var rawSineWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSineWave + 1f) / 2f; // recalculated to go from 0 to 1 and back
        movementFactor = rawSineWave;
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = _startingPosition + offset;
    }
}