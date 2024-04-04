using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D _rbody;
    private Animator _animator;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpHeight = 5f;

    private bool _onTheGround;
    public bool OnTheGround
    {
        private set
        {
            if(_onTheGround != value)
            {
                _onTheGround = value;
                _animator.SetBool("grounded", _onTheGround);
            }
        }
        get => _onTheGround;
    }

    private float _verticalSpeed;
    public float VerticalSpeed
    {
        private set
        {
            if(_verticalSpeed != value)
            {
                _verticalSpeed = value;
                _animator.SetFloat("ySpeed", _verticalSpeed);
            }

        }
        get => _verticalSpeed;
    }

    private float _horizontalMovement;
    public float HorizontalMovement
    {
        private set
        {
            if (value != _horizontalMovement)
            {
                _horizontalMovement = value;

                _animator.SetFloat(
                        "xSpeed", 
                        Mathf.Abs(_horizontalMovement)
                    );

                if(_horizontalMovement != 0 )
                    FacingRight = _horizontalMovement > 0;
            }
        }

        get => _horizontalMovement;

    }

    [SerializeField] private bool _facingRight = true;
    public bool FacingRight
    {
        private set 
        { 
            if(_facingRight != value)
            {
                _facingRight=value;
                Flip()
;
            }
        }
        get => _facingRight;
    }

    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        VerticalSpeed = _rbody.velocity.y;  

        HorizontalMovement = Input.GetAxis("Horizontal");
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rbody.velocity = new Vector2(
                HorizontalMovement * moveSpeed,
                _rbody.velocity.y
            );
    }

    private void GroundCheck()
    {
        RaycastHit2D[] hits = new RaycastHit2D[5];
        int numhits = _rbody.Cast(Vector2.down, hits, 0.5f);
        OnTheGround = numhits > 0;
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && OnTheGround)
        {
            _animator.SetTrigger("jumpTrigger");
            _rbody.AddForce(
                    Vector2.up * jumpHeight, 
                    ForceMode2D.Impulse
                );
        }
    }

    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }
}
