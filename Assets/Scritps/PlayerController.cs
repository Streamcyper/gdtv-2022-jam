using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Collider2D feet;

    public bool isActive = true;

    private Vector2 _moveDirection;
    private Vector2 _rawInput;
    private bool _isJumping;
    private Rigidbody2D _rigidbody;


    private const string PLATFORM_LAYER = "ground";

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Move the player
        _rigidbody.velocity = new Vector2(_rawInput.x * moveSpeed, _rigidbody.velocity.y);

        //Make the player jump
        if (_isJumping)
        {
            _rigidbody.velocity += new Vector2(0f, jumpForce);
            _isJumping = false;
        }
    }

    //Used by the input system 
    private void OnMove(InputValue value)
    {
        if (!isActive) return;

        _rawInput = value.Get<Vector2>();
    }

    //Used by the input system
    private void OnJump(InputValue value)
    {
        if (!isActive) return;

        if (!feet.IsTouchingLayers(LayerMask.GetMask(PLATFORM_LAYER))) return;

        _isJumping = true;
    }
    
}