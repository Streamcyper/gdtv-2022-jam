using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private const string PLATFORM_LAYER = "ground";

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Collider2D feet;

    public bool isActive = true;

    private Rigidbody2D _rigidbody;
    private PlayerInput _playerInput;
    private Soul _soul;
    private Vector2 _rawInput;
    private LayerMask _ground;
    private float _jump;
    private float _holdTime;
    private bool _isJumping;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ground = LayerMask.GetMask(PLATFORM_LAYER);
        _soul = GetComponent<Soul>();
        _playerInput = new PlayerInput();
        _playerInput.Movement.Enable();
        _playerInput.Movement.Jump.canceled += OnJump;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            Move();

            if (_isJumping)
            {
                if (!IsGrounded()) KarmaJump();

                _rigidbody.velocity += new Vector2(0f, _jump);
                _isJumping = false;
            }
        }
    }

    private void KarmaJump()
    {
        _soul.UseKarma(1);
        _jump = jumpForce * 2;
    }

    private void Move()
    {
        _rawInput = _playerInput.Movement.Move.ReadValue<Vector2>();
        _rigidbody.velocity = new Vector2(_rawInput.x * moveSpeed,
            Mathf.Clamp(_rigidbody.velocity.y, -jumpForce * 2, jumpForce * 2));
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isActive)
        {
            _holdTime = Mathf.Clamp((float)context.duration, 0, 1);

            _jump = jumpForce * (1 + _holdTime);
            if (CanJump()) _isJumping = true;
        }
    }

    private bool CanJump()
    {
        return IsGrounded() || HasKarma();
    }

    private bool HasKarma() => _soul.KarmaAmount > 0;

    private bool IsGrounded()
    {
        return feet.IsTouchingLayers(_ground);
    }
}