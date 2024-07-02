using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private LayerMask _layerMask;

    private bool _isGrounded;
    
    private Rigidbody _rbPlayer;

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
    }
    private void Jump()
    {
        if (_isGrounded && (Input.GetAxis("Jump") > 0))
        {
            if (_isGrounded)
            {
            _rbPlayer.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
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
        }
    }
}
