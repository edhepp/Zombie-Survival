using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Input")] 
    [SerializeField] public InputReader _input;
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (InputManager.Instance == null) Instance = this; 
        else Destroy(gameObject);
    }

    #region SetUpInputEvents

    private void OnEnable()
    {
        _input.MoveEvent += OnMove;
        _input.JumpEvent += OnJump;
        _input.JumpCancelledEent += OnJumpCancelled;
        _input.AttackEvent += OnAttack;
    }

    private void OnDisable()
    {
        _input.MoveEvent -= OnMove;
        _input.JumpEvent -= OnJump;
        _input.JumpCancelledEent -= OnJumpCancelled;
        _input.AttackEvent -= OnAttack;
    }

    private void OnMove(Vector2 movement)
    {
        Debug.Log(movement);
    }

    private void OnJump()
    {
        Debug.Log("Jumped = true");
    }

    private void OnJumpCancelled()
    {
        Debug.Log("Jumped = false");
    }

    private void OnAttack()
    {
        Debug.Log("Attacked");
    }

    #endregion
    void Start()
    {
        //Do something
    }

    // Update is called once per frame
    void Update()
    {
        //Do something
    }
}
