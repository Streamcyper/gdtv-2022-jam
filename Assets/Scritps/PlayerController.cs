using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private LayerMask ground;
    [SerializeField] private float speed = 1;
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float jumpForce = 1;
    [SerializeField] private float jumpSpeed = 1;
    [SerializeField] private float fallSpeed = 1;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        ground = LayerMask.GetMask("ground");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetAxisRaw("Vertical") != 0 && CanJump())
            Jump();
    }

    private bool IsGrounded() => _rigidbody2D.IsTouchingLayers(ground);

    private bool CanJump() => IsGrounded();

    private void Jump()
    {
        var _jump = new Vector2(0, jumpForce);
        _rigidbody2D.AddForce(_jump, ForceMode2D.Impulse);
        Mathf.Clamp(_rigidbody2D.velocity.y, fallSpeed, jumpSpeed);
    }

    private void Move()
    {
        float _direction = Input.GetAxisRaw("Horizontal");
        float _amount = (_direction * speed) * Time.deltaTime;
        var _velocity = _rigidbody2D.velocity;
        _velocity += new Vector2(_amount, 0);
        _rigidbody2D.velocity = _velocity;
        Mathf.Clamp(_velocity.x, -maxSpeed, maxSpeed);
    }
}
