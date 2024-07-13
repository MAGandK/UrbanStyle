using UnityEngine;

public class PersonController : MonoBehaviour
{
    #region Bullet
    [SerializeField] private Transform _bulletParent;
    [SerializeField] private Transform _gunTransform;
    [SerializeField] private float _bulletHitMiss = 25f;
    [SerializeField] private LayerMask _ignoreMask;
    #endregion

    [SerializeField] private Transform _cameraTransform;
    private Animator _animator;
    private CharacterController _characterController;
    private FieldOfView _fieldOfView;
    private AnimationController _animationController;
    
    private Vector3 _moveInput;
    private Vector3 _move;
    private Vector2 _currentBleandAnim;
    private Vector2 _animationVelocity;
    [SerializeField] private float _playerSpeed;
    private bool isGround;
    private Vector3 _playerVelocity;
    [SerializeField] private float _jumpHeight;
    private float _gravityValue = -9.81f;
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private float _animSmoothTime = 0.2f;
    [SerializeField] private float _animationPlayTransition;
    private int _moveXAnimationParametrId;
    private int _moveYAnimationParametrId;
    private int _jumpAnimation;
    private int _runAnimationParamId;
    private int _shootAnimationParamId;
    private int _idleAnimationParamId;
    
    public GameObject Gun;
    public Vector2 MoveInput
    {
        set
        {
            _moveInput.x = value.x;
            _moveInput.y = value.y;
        }
    }
    public bool isJump;
    public bool isRun;
    public bool isShoot;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
        _animator = GetComponent<Animator>();
        _moveXAnimationParametrId = Animator.StringToHash("MovementX");
        _moveYAnimationParametrId = Animator.StringToHash("MovementY");
        _jumpAnimation = Animator.StringToHash("Jump");
        _runAnimationParamId = Animator.StringToHash("isRunning");
        _shootAnimationParamId = Animator.StringToHash("isShooting");
        _idleAnimationParamId = Animator.StringToHash("isIdle");
        
        Gun.SetActive(false);
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GroundCheak();
        MovedCharacter();
        JumpCharacter();
        RotateToDirection();
        if (isShoot)
        {
            ShootGun();
        }
    }

    public void OnEnableAnimationEvent(string str)
    {
        Gun.SetActive(true);
    }
    private void GroundCheak()
    {
        isGround = _characterController.isGrounded;
        if (isGround && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0;
        }
    }
    private void JumpCharacter()
    {
        if (isGround && isJump)
        {
            _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            _animator.CrossFade(_jumpAnimation,_animationPlayTransition);
            isJump = false;
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }
    private void MovedCharacter()
    {
        _currentBleandAnim = Vector2.SmoothDamp(_currentBleandAnim,_moveInput, ref _animationVelocity, _animSmoothTime);
        _move = new Vector3(_currentBleandAnim.x, 0, _currentBleandAnim.y);
        _move = _cameraTransform.right * _moveInput.x + _cameraTransform.forward * _moveInput.y;
        _move.y = 0;
        float speed = isRun ? _playerSpeed * 4 : _playerSpeed; 
        _characterController.Move(_move * Time.deltaTime * speed);
        
        _animator.SetFloat(_moveXAnimationParametrId,_currentBleandAnim.x);
        _animator.SetFloat(_moveYAnimationParametrId,_currentBleandAnim.y);
 
        _animator.SetBool(_runAnimationParamId, isRun);
        _animator.SetBool("isWalk", _moveInput != Vector3.zero && !isRun); 
        
        if (_moveInput == Vector3.zero && !isRun && !isShoot)
        {
            _animator.SetBool(_idleAnimationParamId, true);
        }
        else
        {
            _animator.SetBool(_idleAnimationParamId, false);
        }
    }

    private void RotateToDirection()
    {
        if (_moveInput != Vector3.zero)
        {
            Quaternion rotation = Quaternion.Euler(0f,_cameraTransform.eulerAngles.y,0f);
            transform.rotation = Quaternion.Lerp(transform.rotation,rotation, Time.deltaTime * _rotationSpeed);
        }
    }

    public void ShootGun()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPoolesObject();
        if (bullet != null)
        {
            bullet.transform.parent = _bulletParent;
            bullet.transform.position = _gunTransform.position;
            bullet.transform.rotation = _gunTransform.rotation;
            bullet.SetActive(true);

            BulletController bulletController = bullet.GetComponent<BulletController>();
            
            Ray ray = new Ray(transform.position, _cameraTransform.forward);
            
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _ignoreMask)) 
            {
                bulletController.Target = hit.point;
                bulletController.Hit = true;
            }
            else
            {
                bulletController.Target = _cameraTransform.position + _cameraTransform.forward * _bulletHitMiss;
                bulletController.Hit = false;
            }
        
            Debug.DrawLine(_cameraTransform.transform.position, bulletController.Target, Color.magenta);
            _animator.SetBool(_shootAnimationParamId,true);
            _animationController.Shooting();
        }
        else
        {
            Debug.Log("Не удалось получить пулю из пула объектов.");
        }
    }
}
