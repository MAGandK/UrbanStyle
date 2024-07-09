using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class CarControl : MonoBehaviour
{
    [SerializeField] private float _motorTorque = 500f;
    [SerializeField] private float _breakeTorque = 300f;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _steeringRange = 15f;
    [SerializeField] private float _steeringRangeAtMaxSpeed = 10f;
    [SerializeField] private float _centerOfGravitiOffset = -1f;

    private WheelContr[] _wheels;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = Vector3.up * _centerOfGravitiOffset;
        _wheels = GetComponentsInChildren<WheelContr>();
    }

    private void Update()
    {
        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horizontal");

        float forwardSpeed = Vector3.Dot(transform.forward, _rb.velocity);
        float speedFactor = Mathf.InverseLerp(0, _maxSpeed, forwardSpeed);

        float currentMotorTorque = Mathf.Lerp(_motorTorque, 0, speedFactor);
        float currentSteeringRange = Mathf.Lerp(_steeringRange, _steeringRangeAtMaxSpeed, speedFactor);

        bool isAcceleratoin = Mathf.Sign(vAxis) == Mathf.Sign(forwardSpeed);

        foreach (var wheel in _wheels)
        {
            if (wheel._steerable)
            {
                wheel.WheelCollider.steerAngle = hAxis * currentSteeringRange;
            }

            if (isAcceleratoin)
            {
                if (wheel._motorized)
                {
                    wheel.WheelCollider.motorTorque = vAxis * currentMotorTorque;
                }

                wheel.WheelCollider.brakeTorque = 0;
            }
            else
            {
                wheel.WheelCollider.brakeTorque = Mathf.Abs(vAxis) * _breakeTorque;
                wheel.WheelCollider.motorTorque = 0;
            }
        }

    }
}
