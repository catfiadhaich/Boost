using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour
{
    private Vector3 startPosition;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float period = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }
        const float tau = Mathf.PI * 2;
        float cycles = Time.time / period;
        float rawSine = Mathf.Sin(cycles * tau);
        float movementFactor = (rawSine + 1) / 2;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startPosition + offset;

    }
}
