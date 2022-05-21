using System;
using System.Collections;
using System.Collections.Generic;
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

    private Vector2 _moveDirection;
    private Vector2 _rawInput;
    private bool _isJumping;
    private Rigidbody2D _rigidbody;
    private PlayerInput _playerInput;
    private float _jump;
    private LayerMask _ground;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ground = LayerMask.GetMask(PLATFORM_LAYER);
       _playerInput = new PlayerInput();
       _playerInput.Movement.Enable();
       _playerInput.Movement.Jump.canceled += OnJump;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            _rawInput = _playerInput.Movement.Move.ReadValue<Vector2>();
            _rigidbody.velocity = new Vector2(_rawInput.x * moveSpeed, _rigidbody.velocity.y);

            if (_isJumping)
            {
                _rigidbody.velocity += new Vector2(0f, _jump);
                _isJumping = false;
            }
        }
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        
        var _holdTime = (float)context.duration;
        Mathf.Clamp01(_holdTime);

        _jump = jumpForce * (1 + _holdTime);

        if (CanJump())
            _isJumping = true;
    }

    private bool CanJump()
    {
        if (IsGrounded())
            return true;
        return false;
    }

    private bool IsGrounded() => feet.IsTouchingLayers(_ground);
}