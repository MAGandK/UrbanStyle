using System;
using UnityEngine;
using UnityEngine.Pool;

public class PersonController : MonoBehaviour
{
    #region Bullet
    [SerializeField] private Transform _bulletParent;
    [SerializeField] private Transform _gunTransform;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _bulletHitMiss = 25f;
    #endregion

    private CharacterController _characterController;
    
    private Vector3 _moveInput;
    private Vector3 _move;
    private Vector2 _currentBleandAnim;
    private Vector2 _animationVelocity;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _animSmoothTime = 0.2f;
    
    public Vector2 MoveInput
    {
        set
        {
            _moveInput.x = value.x;
            _moveInput.y = value.y;
        }
    }
    public bool isJump;
    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MovedCharacter();
    }

    private void MovedCharacter()
    {
        _currentBleandAnim = Vector2.SmoothDamp(_currentBleandAnim,_moveInput, ref _animationVelocity, _animSmoothTime);
        _move = new Vector3(_currentBleandAnim.x, 0, _currentBleandAnim.y);
        _move = _cameraTransform.right * _moveInput.x + _cameraTransform.forward * _moveInput.y;
        _move.y = 0;
        _characterController.Move(_move * Time.deltaTime * _playerSpeed);
    }

    public void ShootGun()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPoolesObject();
        if (bullet !=null)
        {
            bullet.transform.parent = _bulletParent;
            bullet.transform.position = _gunTransform.position;
            bullet.transform.rotation = _gunTransform.rotation;
            bullet.SetActive(true);
        }

        BulletController bulletController = bullet.GetComponent<BulletController>();
        RaycastHit hit;
        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit,Mathf.Infinity))
        {
            bulletController.Target = hit.point;
            bulletController.Hit = true;
        }
        else
        {
            bulletController.Target = _cameraTransform.position + _cameraTransform.forward * _bulletHitMiss;
            bulletController.Hit = false;
        }
    }
}
