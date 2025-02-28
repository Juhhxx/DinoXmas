using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    [SerializeField] private float     _jumpForce; 
    [SerializeField] private float     _maxJumpTime;
    [SerializeField] private float     _groundCheckDistance;
    [SerializeField] private LayerMask _groundLayer;
    private float                      _jumpTime;
    private bool                       _isGrounded;
    private float                      _defaultGravity;
    private Rigidbody2D                _rb;
    private Animator                   _anim;

    private bool  _isDead = false;
    public bool   IsDead => _isDead;
    private int   _points;
    public int    Points => _points;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        
        _defaultGravity = _rb.gravityScale;
    }

    private void Update()
    {
        if (!_isDead)
        {
            MoveDino();
            UpdateScore();
        }
        else
        {
            Debug.Log("GAME OVER");
            Time.timeScale = 0.0f;
        }
    }
    private void MoveDino()
    {
        CheckIfGrounded();

        float ySpeed = _rb.velocity.y;

        if (Input.GetButtonDown("Jump") && (_isGrounded))
        {
            ySpeed = _jumpForce;
            _rb.gravityScale = 1.0f;
            _jumpTime = Time.time;
        }
        else if (Input.GetButton("Jump") && ((Time.time - _jumpTime) < _maxJumpTime))
        {
            _rb.gravityScale = 1.0f;
        }
        else
        {
            _rb.gravityScale = _defaultGravity;
        }

        _rb.velocity = new Vector2( 0.0f, ySpeed);
        _anim.SetFloat("VelocityY", _rb.velocity.y);
    }
    private void UpdateScore()
    {
        float time = 0;
        time += Time.deltaTime;
        _points += (int) (time * 100) % 100;

    }
    private void CheckIfGrounded()
    {
        RaycastHit2D _rayInfo = Physics2D.Raycast( _rb.position, Vector2.down, 
                                                   _groundCheckDistance, _groundLayer);

        Debug.DrawRay(_rb.position,Vector2.down * _groundCheckDistance, Color.red);

        _isGrounded = _rayInfo;
        _anim.SetBool("IsGrounded", _isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Trees>() != null)
        {
            _isDead = true;
            _anim.SetBool("IsDead", _isDead);
        }
    }
}
