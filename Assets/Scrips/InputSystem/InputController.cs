using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private PersonController _personController;
    
   #region InputAction
   private PlayerInput _inputController;
   private InputAction _actionMove;
   private InputAction _actionJump;
   private InputAction _actionShoot;
   #endregion
   private void Awake()
   {
       _inputController = GetComponent<PlayerInput>();
       _personController = GetComponent<PersonController>();

       _actionMove = _inputController.actions["Move"];
       _actionJump = _inputController.actions["Jump"];
       _actionShoot = _inputController.actions["Shoot"];

       Cursor.lockState = CursorLockMode.Locked;
   }
   private void OnEnable()
   {
       _actionShoot.performed += _ => Shoting();
   }
   private void Shoting()
   {
       //_personController.ShootGun();
   }
   private void Update()
   {
       Moving();
       Jumping();
   }
   private void Jumping()
   {
       if (_actionJump.triggered)
       {
           _personController.isJump = true;
       }
   }
   private void Moving()
   {
       Vector2 input = _actionMove.ReadValue<Vector2>();
       _personController.MoveInput = input;
   }

   private void OnDisable()
   {
       _actionShoot.performed -= _ => Shoting();
   }
}
