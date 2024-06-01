using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateBehaviour : MonoBehaviour, IDamageable, IInteractable
{
    public delegate void ZombieState();
    public static event ZombieState KilledEvent;
    public static event ZombieState TookDamageEvent;

    public delegate void Interacted(ZombieStateBehaviour interacted);
    public static event Interacted FocusFireEvent;
    
    [SerializeField] private float _helth = 100.0f;
    [SerializeField] private float _currentHealth = 0.0f;
    private void Start()
    {
        _currentHealth = _helth;
        _isFocusedOn = false;
    }

    private void OnEnable()
    {
        _currentHealth = _helth;
        _isFocusedOn = false;
    }

    private bool _isFocusedOn = false;
    public void Interact()
    {
        _isFocusedOn = !_isFocusedOn;
        if (_isFocusedOn)
        {
            Debug.Log("Focused fired on", transform);
            FocusFireEvent?.Invoke(this);
        }
        else
        {
            Debug.Log("Lost Focuse on NPC", transform);
            FocusFireEvent?.Invoke(null);
        }
    }
    public void Attack()
    {
        Debug.Log("Attack performed", transform);
        // how will zombies know to attack barriers when close
        // how will zombies know to attack players or barriers
    }
    public void TakeDamage(float damageMultiplier = 25.0f)
    {
        damageMultiplier = Mathf.Abs(damageMultiplier);
        _currentHealth -= damageMultiplier;
        if (_currentHealth <= 0.0f)
        {
            _currentHealth = 0.0f;
            Destroyed();
            return;
        }
        TookDamageEvent?.Invoke();
        Debug.Log("Event request health change");
    }
    public void Destroyed()
    {
        KilledEvent?.Invoke();
        Debug.Log("Killed");
    }
}