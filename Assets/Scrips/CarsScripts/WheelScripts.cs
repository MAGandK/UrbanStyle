using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScripts : MonoBehaviour
{
    [SerializeField] private WheelCollider _fr_Whwwl_Coll;
    [SerializeField] private WheelCollider _fl_Whwwl_Coll;
    [SerializeField] private WheelCollider _br_Whwwl_Coll;
    [SerializeField] private WheelCollider _bl_Whwwl_Coll;

    [SerializeField] private Transform _frTransform;
    [SerializeField] private Transform _flTransform;
    [SerializeField] private Transform _brTransform;
    [SerializeField] private Transform _blTransform;

    [SerializeField] private float _acceleration = 500f;
    [SerializeField] private float _breakeForce = 400f;
    [SerializeField] private float _maxTurnAngle = 15f;

    private float _currentAcceleration;
    private float _currentBreakeForce;

    private float _curentTurnAngle;

    private void FixedUpdate()
    {
        _currentAcceleration = Input.GetAxis("Vertical") * _acceleration;
        
        if (Input.GetKey(KeyCode.Space))
        {
            _currentBreakeForce = _breakeForce;
        }
        else
        {
            _currentBreakeForce = 0f;
        }

        _fr_Whwwl_Coll.motorTorque = _currentAcceleration;
        _fl_Whwwl_Coll.motorTorque = _currentAcceleration;

        _fr_Whwwl_Coll.brakeTorque = _breakeForce;
        _fl_Whwwl_Coll.brakeTorque = _breakeForce;
        _br_Whwwl_Coll.brakeTorque = _breakeForce;
        _bl_Whwwl_Coll.brakeTorque = _breakeForce;

        _curentTurnAngle = Input.GetAxis("Horizontal") * _maxTurnAngle;
        _fr_Whwwl_Coll.steerAngle = _curentTurnAngle;
        _fl_Whwwl_Coll.steerAngle = _curentTurnAngle;
    }
}
