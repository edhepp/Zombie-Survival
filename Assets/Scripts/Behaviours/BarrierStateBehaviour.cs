using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BarrierStateBehaviour : MonoBehaviour, IInteractable, IDamageable, IRepairable
{
    [SerializeField]
    private float _health = 100.0f;
    [SerializeField]
    private float _currentHealth = 0.0f;
    void Start()
    {
        _currentHealth = _health;
        // Listen for events or
        // Allow public access
    }

    private bool _isRepairBusy = false;
    [ContextMenu(nameof(Interact))]
    public void Interact()
    {
        // When click request for an engineer to do repairs (event)
        // if repair is current request stop else request repair
        if (!_isRepairBusy)
        {
            //event Request Agent to repair
            _isRepairBusy = true;
            Debug.Log("Send Repair request");
        }
        else
        {
            //event Request Agent to stop Rapairs
            _isRepairBusy = false;
            Debug.Log("Requset to Stop Rapair");
        }
    }
    [ContextMenu(nameof(TakeDamage))]
    public void TakeDamage(float damageMultiplier = 25.0f)
    {
        _currentHealth -= damageMultiplier;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Destroyed();
            return;
        }
        Debug.Log("Event Request Barrier Damaged");
        // Event (int)
            // (Update UI) (Call Audio SFX) (Call VFX)
        //Call this method when Zombie Attacks collider via OnTrigger Enter
    }

    public void Destroyed()
    {
        //Send event for destoryed (Update UI Warning) (Audio) (FX)
        Debug.Log("Fully Destroyed");
    }

    [ContextMenu(nameof(Repair))]
    public void Repair(float multiplier = 10.0f)
    {
        _currentHealth += multiplier;
        if (_currentHealth >= _health)
        {
            _currentHealth = _health;
            _isRepairBusy = false;
            FullyRepaired();
            return;
        }
        Debug.Log("Event Request Increment Audio, SFX, VFX");
        // public access
        // Request UI Health update (increments)
        // some logic For a complete job (Audio, UI update)
        // Send Event onComplete or Canceled
            //Complete (UI update) (Player Stop working)
            //Cancel (Player stop working)
    }
    public void FullyRepaired()
    {
        //Send Event for Fully Repaired
        Debug.Log("Fully Repaired");
    }
}

