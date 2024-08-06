using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ECharacterState
{
    None,
    Idle,
    Move,
    MoveEnd,
    Jump,
}

public class CharacterMoveComponent : MonoBehaviour
{
    //角色属性参数
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float rotateSpeed = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float stRunTime = 0.1f;
    [SerializeField] private float edRunTime = 0.1f;
    
    private ECharacterState _characterState;
    //角色相关组件
    private CharacterController _controller;
    private Animator _animator;
    [SerializeField] private Camera _followCamera;
    //临时变量
    private Vector3 _moveDir;
    private float _ySpeed;
    private bool _isJumping;

    private float _runTimer = 0;
    
    //动画值
    private static readonly int MoveValue = Animator.StringToHash("MoveValue");

    private ECharacterState CharacterState { 
        get=>_characterState;
        set
        {
            if (value != _characterState)
            {
                _characterState = value;
                if (_characterState == ECharacterState.Idle)
                {
                    _animator.SetFloat(MoveValue, -1);
                }
                else if (_characterState == ECharacterState.Move)
                {
                    _animator.SetFloat(MoveValue, 1);
                    _runTimer = stRunTime;
                }
                else if (_characterState == ECharacterState.MoveEnd)
                {
                    _runTimer = edRunTime;
                }
                else if (_characterState == ECharacterState.Jump)
                {
                    
                }
            }
        }
    }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        Move();
        Rotate();
    }
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            CharacterState = ECharacterState.Move;
        }
        else
        {
            if (CharacterState == ECharacterState.Move)
            {
                CharacterState = ECharacterState.MoveEnd;
            }
            else
            {
                CharacterState = ECharacterState.Idle;
            }
        }
        float speed = moveSpeed;
        if (CharacterState == ECharacterState.Move)
        {
            if (_runTimer > 0)
            {
                _runTimer -= Time.deltaTime;
                speed = Mathf.Lerp(0, moveSpeed, 1 -_runTimer / stRunTime);
            }
        }
        else if (CharacterState == ECharacterState.MoveEnd)
        {
            if (_runTimer > 0)
            {
                _runTimer -= Time.deltaTime;
                if (_runTimer <= 0)
                {
                    CharacterState = ECharacterState.Idle;
                }
            }
        }
        
        // _moveDir = transform.TransformDirection(new Vector3(h, 0, v));
        //临时写法
        Rotate();
        _moveDir *= speed;
        if (_controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _ySpeed = jumpForce;
                _isJumping = true;
            }
        }
        else
        {
            _ySpeed -= gravity * Time.deltaTime;
        }
        _moveDir.y = _ySpeed;
        _controller.Move(_moveDir * Time.deltaTime);
    }
    private void Rotate()
    {
        //获取相机正前方方向
        Vector3 cameraForward = _followCamera.transform.forward;
        //保持y轴不变
        cameraForward.y = 0;
        //获取相机右方向
        Vector3 cameraRight = _followCamera.transform.right;
        //保持y轴不变
        cameraRight.y = 0;
        //获取输入方向
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        _moveDir = cameraForward * v + cameraRight * h;
        _moveDir.Normalize();
        if (_moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (_isJumping)
        {
            _ySpeed = 0;
            _isJumping = false;
        }
    }

    private void OnAnimatorMove()
    {
        Vector3 rootPos = _animator.deltaPosition;
        _controller.Move( rootPos * 100);
        
        transform.rotation *= _animator.deltaRotation  ;
    }
}