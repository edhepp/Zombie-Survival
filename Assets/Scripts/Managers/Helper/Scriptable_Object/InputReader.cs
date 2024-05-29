using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Input/Input Reader", fileName = "Input_Reader")]
public class InputReader : ScriptableObject
{
    [SerializeField] private InputActionAsset _inputAsset;

    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction JumpEvent;
    public event UnityAction JumpCancelledEent;
    public event UnityAction AttackEvent;
    
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;

    private void OnEnable()
    {
        _moveAction = _inputAsset.FindAction("Move");
        _jumpAction = _inputAsset.FindAction("Jump");
        _attackAction = _inputAsset.FindAction("Fire");

        _moveAction.started += OnMove;
        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;

        _jumpAction.started += OnJump;
        _jumpAction.performed += OnJump;
        _jumpAction.canceled += OnJump;

        _attackAction.started += OnAttack;
        _attackAction.performed += OnAttack;
        _attackAction.canceled += OnAttack;
        
        _moveAction.Enable();
        _jumpAction.Enable();
        _attackAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.started -= OnMove;
        _moveAction.performed -= OnMove;
        _moveAction.canceled -= OnMove;

        _jumpAction.started -= OnJump;
        _jumpAction.performed -= OnJump;
        _jumpAction.canceled -= OnJump;

        _attackAction.started -= OnAttack;
        _attackAction.performed -= OnAttack;
        _attackAction.canceled -= OnAttack;

        _moveAction.Disable();
        _jumpAction.Disable();
        _attackAction.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        MoveEvent?.Invoke(ctx.ReadValue<Vector2>());
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        if(JumpEvent != null && ctx.started)
            JumpEvent?.Invoke();
        if (JumpCancelledEent != null && ctx.canceled)
        {
            JumpCancelledEent?.Invoke();
        }
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        if(AttackEvent != null && ctx.started)
            AttackEvent?.Invoke();
    }
}
