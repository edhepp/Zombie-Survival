using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BarrierStateBehaviour : MonoBehaviour, IInteractable, IDamageable, IRepairable
{
    public delegate void BarrierInteraction(BarrierStateBehaviour prefab);

    public static event BarrierInteraction InteractEvent;
    public static event BarrierInteraction FullRepairEvent;
    public static event BarrierInteraction DestoryedEvent;


    public delegate void BarrierHealth(BarrierStateBehaviour barrier, float health);
    public static event BarrierHealth TookDamageEvent;
    public static event BarrierHealth RepairEvent;
    
    [SerializeField]
    private float _health = 100.0f;
    [SerializeField]
    private float _currentHealth = 0.0f;
    [SerializeField] 
    private float _zOffsetTargetPosition = 0.7f;
    void Start()
    {
        //Listen for repairer assignment event request
        _currentHealth = _health;
        float targetOffset = transform.position.z - _zOffsetTargetPosition;
        TargetPosition = new Vector3(transform.position.x, transform.position.y, targetOffset);
        // Listen for events or
        // Allow public access
        RepairEvent?.Invoke(this, _currentHealth);
    }

    private bool _isRepairBusy = false;
    public void Interact()
    {
        //Bug: When clicking another barrier othet then "this" this state doesn't change. (cancel repairing)
        // When click request for an engineer to do repairs (event)
        // if repair is current request stop else request repair
        if (_currentHealth >= _health)
            return;
        InteractEvent?.Invoke(this);
    }
    public void TakeDamage(float damageMultiplier = 25.0f)
    {
        damageMultiplier = Mathf.Abs(damageMultiplier);
        _currentHealth -= damageMultiplier;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Destroyed();
            return;
        }
        TookDamageEvent?.Invoke(this, _currentHealth);
        Debug.Log("Event Request Barrier Damaged");
        // Event (int)
            // (Update UI) (Call Audio SFX) (Call VFX)
        //Call this method when Zombie Attacks collider via OnTrigger Enter
    }
    public Vector3 TargetPosition { get; set; }
    
    public void Repair(float multiplier = 10.0f)
    {
        if (Collider.enabled == false) Collider.enabled = true;
        _currentHealth += multiplier;
        if (_currentHealth >= _health)
        {
            _currentHealth = _health;
            _isRepairBusy = false;
            FullyRepaired();
            return;
        }
        RepairEvent?.Invoke(this, _currentHealth);
        Debug.Log("Event Request Increment Audio, SFX, VFX");
        // public access
        // Request UI Health update (increments)
        // some logic For a complete job (Audio, UI update)
        // Send Event onComplete or Canceled
        // Complete (UI update) (Player Stop working)
        // Cancel (Player stop working)
    }
    public void FullyRepaired()
    {
        FullRepairEvent?.Invoke(this);
        //Send Event for Fully Repaired
        Debug.Log("Fully Repaired");
    }
    [SerializeField]
    private BoxCollider Collider;
    public void Destroyed()
    {
        //Bug: Barrier loses interactable when collider is dissabled.
        Collider.enabled = false;
        //Send event for destoryed (Update UI Warning) (Audio) (FX)
        DestoryedEvent?.Invoke(this);
        Debug.Log("Fully Destroyed");
    }
}

