using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : MonoBehaviour, IDamageable, IInteractable
{
    [SerializeField] private float _helth = 100.0f;

    [SerializeField] private float _currentHealth = 0.0f;

    private void Start()
    {
        _currentHealth = _helth;
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
    public void TakeDamage(float damageMultiplier)
    {
        throw new System.NotImplementedException();
    }

    public void Destroyed()
    {
        throw new System.NotImplementedException();
    }
}
