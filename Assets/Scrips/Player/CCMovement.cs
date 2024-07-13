using UnityEngine;

public class CCMovement : MonoBehaviour
{
   [SerializeField] private float _walkSpeed = 3f;
   [SerializeField] private float _runSpeed = 6f;
   [SerializeField] private float _mouseSensetivity = 100;
   [SerializeField] private float _gravity = -9.81f;

   private CharacterController _characterController;
   private Vector3 _velocity;
   private float _rotationY = 0;
   private float _rotationX = 0;
   

   [SerializeField] private Transform _cameraTransform;
   private AnimationController _animationController;
   private void Start()
   {
      _characterController = GetComponent<CharacterController>();
      _animationController = GetComponent<AnimationController>();
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
      
      _velocity.y += _gravity * Time.deltaTime;
      _characterController.Move(_velocity * Time.deltaTime);
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
      float speed = Input.GetKey(KeyCode.RightShift) ? _runSpeed : _walkSpeed; 
      _characterController.Move(move * speed * Time.deltaTime);
      
      if (move != Vector3.zero)
      {
         _animationController.Move(speed > _walkSpeed);
      }
      else
      {
         _animationController.Idle();
      }
      if (Input.GetButtonDown("Jump") && _characterController.isGrounded) 
      {
         _velocity.y = Mathf.Sqrt(-2f * _gravity * 2f); 
         _animationController.Jump();
      }
      else
      {
         _animationController.StopJump(); 
      }
   }
}
