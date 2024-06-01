using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Input")] 
    [SerializeField] public InputReader _input;

    [SerializeField] private LayerMask _interactableLayers;
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        _camera = Camera.main;
        if (InputManager.Instance == null) Instance = this; 
        else Destroy(gameObject);
    }
    #region SetUpInputEvents

    private void OnEnable()
    {
        _input.InteractEvent += OnInteraction;
        _input.JumpCancelledEent += OnJumpCancelled;
        _input.AttackEvent += OnAttack;
        _input.MoveEvent += OnMove;
        _input.JumpEvent += OnJump;
    }


    private void OnDisable()
    {
        _input.InteractEvent -= OnInteraction;
        _input.JumpCancelledEent -= OnJumpCancelled;
        _input.AttackEvent -= OnAttack;
        _input.MoveEvent -= OnMove;
        _input.JumpEvent -= OnJump;
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
        Debug.Log("Attacked ");
    }

    private Camera _camera;
    private void OnInteraction()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        //Debug.Log("click location " + mousePosition);
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _interactableLayers))
        {
            GameObject clickedObject = hit.collider.gameObject;
            if (clickedObject.CompareTag("Interactable"))
            {
                IInteractable interactable = clickedObject.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
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
