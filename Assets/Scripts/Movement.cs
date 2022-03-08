using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float thrust = 1000.0f;
    [SerializeField] private float minThrust = 500.0f;
    [SerializeField] private float maxThrust = 2000.0f;
    [SerializeField] private float rotationRate = 50f;
    [SerializeField] private float lateralThrust = 200f;
 
    private Rigidbody rocketBody;

    private void Awake() {
        rocketBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        ProcessThrustPower();
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrustPower() {
        if (Input.GetKey(KeyCode.W)) {
            thrust += 10;
        } else if (Input.GetKey(KeyCode.S)) {
            thrust -= 10;
        }
        thrust = Mathf.Clamp(thrust, minThrust, maxThrust);
    }
    private void ProcessThrust() {
        
        float lateral = 0f;
        if (Input.GetKey(KeyCode.Q)) {
            lateral = lateralThrust * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.E)) {
            lateral = -lateralThrust * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Return)) {
            float forwardThrust = thrust * Time.deltaTime;
            rocketBody.AddRelativeForce(lateral, forwardThrust, 0);
        } 
        
    }

    private void ProcessRotation() {
        
        if (Input.GetKey(KeyCode.A)) {
            ApplyRotation(1);
        } else if (Input.GetKey(KeyCode.D)) {
            ApplyRotation(-1);
        }
    }

    private void ApplyRotation(int rot) {
        float effectiveRotation = rotationRate * Time.deltaTime * rot;
        transform.Rotate(0, 0, effectiveRotation);
    }
}
