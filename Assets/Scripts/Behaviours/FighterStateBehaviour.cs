using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class FighterStateBehaviour : MonoBehaviour
{
    private Transform _player;
    private Transform _barrier;
    private Vector3 _originalPost;
    private Vector3 _barrierLocation;
    void Start()
    {
        _player = transform;
        _originalPost = _player.transform.position;
        //Listen for a repair request
        BarrierStateBehaviour.InteractEvent += (x) => MoveToRepair(x);
    }
    private void MoveToRepair(BarrierStateBehaviour barrierState)
    {
        if (barrierState is null)
        {
            Debug.Log("Stop repair");
            _barrier = null;
            _barrierLocation = _originalPost;
            return;
        }

        _barrierLocation = new Vector3(barrierState.TargetPosition.x, transform.position.y,
            barrierState.TargetPosition.z);
        _barrier = barrierState.transform;
        Debug.Log("move to Repair site");
        //Change animation state to repair
    }
    private void FixedUpdate()
    {
        //Todo: Move this Logic to a Move script create an event or interface.
        _player.transform.position = Vector3.MoveTowards(
            _player.transform.position, 
            _barrier is null ? _originalPost : _barrierLocation, 
            1 * Time.fixedDeltaTime
            );
    }
    private void OnTriggerEnter(Collider other)
    {
        //Todo: Move this logic to another script Called CanRepair.cs
        var repairable = other.GetComponent<IRepairable>();
        if (repairable != null)
        {
            var barrierState = repairable as BarrierStateBehaviour;
            if(barrierState is null) Debug.Log("Barrier State Empty", transform);
            barrierState?.Repair();
            //Todo: if NPC is Engineer then 2X repair with max 85% fast the rest 15% slow
            //Todo: if NPC is Gunner then 1X repair with 50% fast the rest 50% slow
            //Shoot ray cast to repair? or use a box coolider with hammer animation
        }
    }
}