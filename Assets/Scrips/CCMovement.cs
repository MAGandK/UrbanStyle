using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CCMovement : MonoBehaviour
{
   [SerializeField] private float _speed;
   [SerializeField] private float _mouseSensetivity = 100;
   [SerializeField] private float _gravity = -9.81f;

   private CharacterController _characterController;
   private Vector3 _velocity;
   private float _rotationY = 0;
   private float _rotationX = 0;

   [SerializeField] private Transform _cameraTransform;

   private void Start()
   {
      _characterController = GetComponent<CharacterController>();
      Cursor.lockState = CursorLockMode.Locked;
   }

   private void FixedUpdate()
   {
       Movement();
       RotateCharacter();
       ApplyGravity();
   }

   private void ApplyGravity()
   {
      if (_characterController.isGrounded && _velocity.y < 0)
      {
         _velocity.y = -2f;
      }
   }

   private void RotateCharacter()
   {
      float mouseX = Input.GetAxis("Mouse X") * _mouseSensetivity * Time.deltaTime;
      float mouseY = Input.GetAxis("Mouse Y") * _mouseSensetivity * Time.deltaTime;

      _rotationY += mouseX;
      transform.localRotation = Quaternion.Euler(0f, _rotationY, 0f);

      _rotationX -= mouseY;
      _rotationX = Mathf.Clamp(_rotationX, -60f, 60f);
      
      _cameraTransform.localRotation = Quaternion.Euler(_rotationX, 0,0);
      
   }

   private void Movement()
   {
      float moveX = Input.GetAxis("Horizontal");
      float moveZ = Input.GetAxis("Vertical");

      Vector3 move = transform.right * moveX + transform.forward * moveZ;
      _characterController.Move(move * _speed * Time.deltaTime);
   }
}
