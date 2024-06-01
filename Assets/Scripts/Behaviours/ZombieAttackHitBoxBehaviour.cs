using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ZombieAttackHitBoxBehaviour : MonoBehaviour
{
    public delegate void HitBoxCollider(List<IDamageable> interactables);

    public static event HitBoxCollider HitBoxInteractables;

    private List<IDamageable> _hitList = new();

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float _attackDuration = 0.25f;
    private float _currentTime = 0.0f;

    private void OnEnable()
    {
        _currentTime = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f, quaternion.identity, layerMask);

        if (colliders.Length <= 0)
        {
            Debug.LogWarning("Found Colliders");
            return;
        }
        foreach (var collider in colliders)
        {
            var interactable = collider.GetComponent<IDamageable>();
            if (interactable != null && !_hitList.Contains(interactable))
            {
                _hitList.Add(interactable);
            }
        }
        //Call take damage on Interactable.TakeDamage(float damage);
        Debug.LogWarning("Sending Collider list of " + colliders.Length);
        HitBoxInteractables?.Invoke(_hitList);
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        //Disable attack after 0.25f
        if (_currentTime + _attackDuration < Time.time)
        {
            gameObject.SetActive(false);
        }
    }
}
