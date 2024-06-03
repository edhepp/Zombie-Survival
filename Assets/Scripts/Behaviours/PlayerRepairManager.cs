using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerRepairManager : MonoBehaviour
{
    [SerializeField] private Transform _repairZone;
    private bool _isRepairing = false;
    private bool _repairbox = false;
    private void Start()
    {
        FighterStateBehaviour.AssignThis += (x, y) => EnableRepair();
    }

    private void OnDisable()
    {
        FighterStateBehaviour.AssignThis -= (x, y) => EnableRepair();
    }

    private void EnableRepair()
    {
        _isRepairing = true;
    }

    [SerializeField]
    private float _intervals = 0.25f;
    private float _currentTime = 0;
    private void FixedUpdate()
    {
        if (_isRepairing && _currentTime + _intervals < Time.time)
        {
            _currentTime = Time.time;
            _repairZone.gameObject.SetActive(!_repairbox);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Repairable"))
        {
            other.enabled = true;
        }
    }
}
