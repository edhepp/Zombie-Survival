using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreBarrier : MonoBehaviour
{
    [SerializeField] private BoxCollider _reenableInteractbable;
    // Start is called before the first frame update
    public void EnableBarrier()
    {
        _reenableInteractbable.enabled = true;
    }
}
