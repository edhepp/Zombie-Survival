using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class FighterStateBehaviour : MonoBehaviour
{
    public delegate void AssignRepairer(FighterStateBehaviour AssignJob, BarrierStateBehaviour toBarrier);

    public static event AssignRepairer AssignThis;
    private Transform _player;
    private Transform _barrier;
    private Vector3 _originalPost;
    private Vector3 _barrierLocation;
    void Start()
    {
        _player = transform;
        _originalPost = _player.transform.position;
        //Listen for a repair request
        BarrierStateBehaviour.InteractEvent += (x) => RequestRepair(x);
    }

    public void RepairCompleteOrCanceled()
    {
        _currentBarrier = null;
    }
    // if Repairer is already assign find someone else.
    // if you only have one engineer then manually move him to the destination.
    // 
    private BarrierStateBehaviour _currentBarrier;

    private void RequestRepair(BarrierStateBehaviour barrier)
    {
        if (barrier is null) return;
        if (_currentBarrier && _currentBarrier != barrier)
        {
            Debug.LogWarning("Already busy", transform);
            return;
        }
        _currentBarrier = barrier;
        AssignThis?.Invoke(this, barrier);
    }
    public void AcceptedRepairRequest()
    {
        if (_currentBarrier is null) return;
        _barrierLocation = new Vector3(_currentBarrier.TargetPosition.x, transform.position.y,
            _currentBarrier.TargetPosition.z);
        Debug.Log("move to Repair site");
        //Change animation state to repair
    }
    private void FixedUpdate()
    {
        //Todo: Move this Logic to a Move script create an event or interface.
        _player.transform.position = Vector3.MoveTowards(
            _player.transform.position, 
            _currentBarrier is null ? _originalPost : _barrierLocation, 
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