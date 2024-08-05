using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimComponent : MonoBehaviour
{
    private Animator _animator;
    
    private Action<Vector3> _onMove =null;
    
    private Action<Quaternion> _onRotate =null;
    
    //动画值
    private static readonly int MoveValue = Animator.StringToHash("MoveValue");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        _onMove?.Invoke(_animator.deltaPosition);
        _onRotate?.Invoke(_animator.deltaRotation);
    }
    
    public void AddMoveListener(Action<Vector3> onMove)
    {
        _onMove += onMove;
    }
    
    public void RemoveMoveListener(Action<Vector3> onMove)
    {
        _onMove -= onMove;
    }
    
    public void AddRotateListener(Action<Quaternion> onRotate)
    {
        _onRotate += onRotate;
    }
    
    public void RemoveRotateListener(Action<Quaternion> onRotate)
    {
        _onRotate -= onRotate;
    }
    
    public void SetMoveValue(float value)
    {
        _animator.SetFloat(MoveValue, value);
    }

}
