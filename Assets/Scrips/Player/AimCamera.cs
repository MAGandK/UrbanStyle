using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimCamera : MonoBehaviour
{
   [SerializeField] private PlayerInput _playerInput;
   private CinemachineVirtualCamera _virtualCamera;
   [SerializeField] private int _priorityBoost = 10;
   private InputAction _aimAction;

   private void Awake()
   {
      _virtualCamera = GetComponent<CinemachineVirtualCamera>();
      _aimAction = _playerInput.actions["Aim"];
   }

   private void OnEnable()
   {
      _aimAction.performed += _ => StartAim();
      _aimAction.canceled += _ => CanceAim();
   }

   private void CanceAim()
   {
      _virtualCamera.Priority -= _priorityBoost;
   }

   private void StartAim()
   {
      _virtualCamera.Priority += _priorityBoost;
   }

   private void OnDisable()
   {
      _aimAction.performed -= _ => StartAim();
      _aimAction.canceled -= _ => CanceAim();
   }
}