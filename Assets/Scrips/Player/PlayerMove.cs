using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private LayerMask _layerMask;

    private bool _isGrounded;
    
    private Rigidbody _rbPlayer;
    private AnimationController _animationController;

    private Vector3 _movementVector
    {
        get
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            return new Vector3(horizontal, 0, vertical);
        }
    }

    private void Start()
    {
        _rbPlayer = GetComponent<Rigidbody>();
        _rbPlayer.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _animationController = GetComponent<AnimationController>(); 
        if (_layerMask == gameObject.layer)
        {
            Debug.Log("Player SortingLayer must be different from Ground SortingLayer");
        }
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
    }

    private void Movement()
    {
        _rbPlayer.AddForce(_movementVector * _speed, ForceMode.Impulse);
        
        if (_movementVector != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.RightShift))
            {
                Debug.Log("Running");
                _animationController.Move(true); 
            }
            else
            {
                Debug.Log("Walking");
                _animationController.Move(false); 
            }
        }
        else
        {
            _animationController.Idle();
        }
    }
    private void Jump()
    {
        if (_isGrounded && (Input.GetAxis("Jump") > 0))
        {
                _rbPlayer.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            
                _animationController.Jump();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        IsGroundedUpdate(other, true);
    }

    private void OnCollisionExit(Collision other)
    {
        IsGroundedUpdate(other, false);
    }

    private void IsGroundedUpdate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = value;
            if (value)
            {
                _animationController.StopJump(); 
            }
        }
    }
}
