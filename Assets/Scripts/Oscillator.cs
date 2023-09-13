using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    const float pi = Mathf.PI;

    Vector3 startPosition;
    // new Vector (3,0,0) is a good number after several testings
    [SerializeField] Vector3 moveFactor = new Vector3(3, 0, 0);
    [SerializeField][Range(0, 1)] float moveOscillator;

    [SerializeField] float period = 4f;

    void Start()
    {
       startPosition = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        // moveOscillator is a number between 0-1 --> time-dependant
        moveOscillator = 0.5f * Mathf.Sin(2 * pi * cycles) + 0.5f;

        transform.position = startPosition + (moveFactor * moveOscillator);
    }
}
