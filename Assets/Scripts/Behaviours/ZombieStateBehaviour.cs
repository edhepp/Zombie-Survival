using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateBehaviour : MonoBehaviour, IDamageable, IInteractable
{
    //Feature: smart zombies
    public delegate void ZombieState();
    public static event ZombieState KilledEvent;
    public static event ZombieState TookDamageEvent;

    public delegate void Interacted(ZombieStateBehaviour interacted);
    public static event Interacted FocusFireEvent;
    
    [SerializeField] private float _helth = 100.0f;
    [SerializeField] private float _currentHealth = 0.0f;
    private void Start()
    {
        BarrierStateBehaviour.DestoryedEvent += (x) => BarrierDestoryed(x);
        _currentHealth = _helth;
        _isFocusedOn = false;
    }

    private void OnEnable()
    {
        //Todo: reset all parameters needed
        _currentHealth = _helth;
        _isFocusedOn = false;
    }

    private void BarrierDestoryed(BarrierStateBehaviour barrier)
    {
        //Get mediator list of destroyed barriers then pick one to go towards.
        //if the zombie enables as a smart zombie. 5% chance to be smart per enable.
        //20% chance on exsiting zombies present notice the destoryed barrier.
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
    [SerializeField] private Transform _attackHitBox;
    //Todo: if Zombie reaches Z 2.75 or less start attacking.
    [SerializeField] private float _attackSpeed = 0.25f;
    private float _CurrentAttackTime = 0;
    private void FixedUpdate()
    {
        if (transform.position.z <= 2.75f)
        {
            if (_CurrentAttackTime + _attackSpeed < Time.time)
            {
                Attack();
            }
        }
    }

    public void Attack()
    {
        if (_attackHitBox is null)
        {
            Debug.LogWarning("_attackHitBox not set", transform);
            return;
        }
        _CurrentAttackTime = Time.time;
        _attackHitBox.gameObject.SetActive(true);
        
        //Rule: 1 zombie cant destory a barrier faster than it can be repaired.
        // If zombie destorys one barrier ignore all barrier colliders and accept any player colliders.
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
        gameObject.SetActive(false);
        Debug.Log("Killed");
    }
}