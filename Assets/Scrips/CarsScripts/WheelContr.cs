using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelContr : MonoBehaviour
{
   public Transform wheelModel;
   public WheelCollider WheelCollider;

   public bool _steerable;
   public bool _motorized;

   private Vector3 _position;
   private Quaternion _rotation;

   private void Start()
   {
      WheelCollider = GetComponent<WheelCollider>();
   }

   private void Update()
   {
      WheelCollider.GetWorldPose(out _position, out _rotation);
      wheelModel.transform.position = _position;
      wheelModel.transform.rotation = _rotation;
   }
}
