using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

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
        _boxCollider = GetComponent<BoxCollider>();
    }

    [SerializeField] private BoxCollider _boxCollider;
    private void OnTriggerEnter(Collider other)
    {
        //LeftOff: 
        Vector3 boxCenter = transform.position + _boxCollider.center;
        Vector3 boxSize = transform.localScale / 2;
        
        Collider[] colliders = Physics.OverlapBox(boxCenter, boxSize, quaternion.identity, layerMask);
        Debug.Log(other.name, other.transform);
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
        foreach (var collider in colliders)
        {
            Debug.Log(collider.name, collider.transform);
        }
        HitBoxInteractables?.Invoke(_hitList);
        _hitList.Clear();
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
