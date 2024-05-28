using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRepairable
{
    public Vector3 TargetPosition { get; set; }
    public void Repair(float multiplier);
}
