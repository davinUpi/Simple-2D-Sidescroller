using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{

    const float GROUND_DETECTION_RANGE = 0.3f;

    private Rigidbody2D _rbody;
    private Collider2D _collider;
    private Animator _animator;

    [SerializeField] private float MoveSpeed = 10f;
    [SerializeField] private float JumpPower = 5f;

    private bool _faceRight = true; 
    public bool FaceRight
    {
        private set 
        { 
            if(_faceRight != value)
            {
                _faceRight = value;
                Vector3 temp = transform.localScale;
                temp.x *= -1;
                transform.localScale = temp;
            }
        }
        get { return _faceRight; }
    }

    private float _horizontalMove = 0;
    public float HorizontalMove
    {
        private set
        {
            if(value !=  _horizontalMove)
            {
                _horizontalMove = value;

                if (_horizontalMove != 0)
                    FaceRight = _horizontalMove > 0;

                _animator.SetBool("IsRunning", _horizontalMove != 0);
            }
        }
        get 
        { 
            return _horizontalMove;
        }
    }

    private bool _onGround = true;
    public bool OnGround
    {
        private set
        {
            _onGround = value;
            _animator.SetBool("OnGround", _onGround);
        }
        get { return _onGround; }
    }

    #region UNITY_METHODS

    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
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
        _animator.SetFloat("VSpeed", _rbody.velocity.y);

        HorizontalMove = Input.GetAxis("Horizontal");
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }
    #endregion

    private void Move()
    {
        _rbody.velocity = new Vector2(
                HorizontalMove * MoveSpeed,
                _rbody.velocity.y
            );
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && OnGround)
        {
            _rbody.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            _animator.SetTrigger("Jump");
        }
    }

    private void GroundCheck()
    {
        RaycastHit2D[] hits = new RaycastHit2D[5];
        int numhhits = _rbody.Cast(Vector2.down, hits, GROUND_DETECTION_RANGE);
        OnGround = numhhits > 0;
    }
}
