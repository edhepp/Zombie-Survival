using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierHealthBehaviour : MonoBehaviour
{
    [SerializeField] private float _health = 100.0f;
    private Renderer _fenceRenderer;
    private Material _fenceMaterial;

    private Color _fullHealthColor;
    private Color _zeroHealthColor = Color.red;
    void Awake()
    {
        _fenceRenderer = GetComponent<Renderer>();

        _fenceMaterial = Instantiate(_fenceRenderer.material);
        _fullHealthColor = _fenceMaterial.color;
        _fenceRenderer.material = _fenceMaterial;
    }

    private void Start()
    {
        //Listen to barrier health events
        BarrierStateBehaviour.TookDamageEvent += (x, y) => UpdateHealth(x, y);
        BarrierStateBehaviour.RepairEvent += (x, y) => UpdateHealth(x, y);
    }

    private void UpdateColor()
    {
        Color currentColor = Color.Lerp(_zeroHealthColor, _fullHealthColor, _health/100.0f);
        _fenceMaterial.color = currentColor;
    }
    private void UpdateHealth(BarrierStateBehaviour barrier, float health)
    {
        if (barrier.gameObject != this.gameObject) return;
        _health = health;
        UpdateColor();
    }
}
