using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BarrierEventMediator : MonoBehaviour
{
    // Listen for all events that are exchanged between Fighers and Barriers
    // do some logic on those events.
    private Dictionary<BarrierStateBehaviour, FighterStateBehaviour> _barrierRepairSystem;

    private void Awake()
    {
        _barrierRepairSystem = new();
    }

    void Start()
    {
        // Listen for relivent events
        // Listen to a user interaction event
            // search for existing event if list is greater than 0
            // register that events script
            // listen for a Fighter Repair request and assign that script.
            // if another barrier is clicked repeat the above but skip the current occupied fighter.
            // if the same registered barrier is clicked cancel it and its fighter.
            // if the repair is complete listen for that repair and assign fighter as complete.
            // the repair in Completed
            // Logic Repair Barrier to qulified Engineer properties, finish repair and reassign it to complete
            //Bug if available Fighers are low Must cancel the last one to do repairs.
            FighterStateBehaviour.AssignThis += (x, y) => CheckForDuplicatesThenAdd(x, y);
            BarrierStateBehaviour.FullRepairEvent += (x) => RepairComplete(x);
    }
    //Bug: CheckForDuplicatesThenAdd Spam click unregistered while Unit was repairing and fully fixed request failed
        //to find barrier in dictionary
    
    private void CheckForDuplicatesThenAdd(FighterStateBehaviour fighter, BarrierStateBehaviour barrier)
    {
        if (_barrierRepairSystem.Count > 0)
        { 
            var result = _barrierRepairSystem.FirstOrDefault(x => x.Key == barrier);
            if(!result.Equals(default(KeyValuePair<BarrierStateBehaviour, FighterStateBehaviour>)))
            {
                if (result.Value == fighter)
                {
                    _barrierRepairSystem.Remove(result.Key);
                    CanceledRepair(barrier, fighter);
                    Debug.LogWarning("Barrier removed " + _barrierRepairSystem.Count, fighter.transform);
                    return;
                }
                fighter.RepairCompleteOrCanceled();
                Debug.LogWarning("Barrier is busy Request ignored " + _barrierRepairSystem.Count, fighter.transform);
                return;
            }
        }
        _barrierRepairSystem.Add(barrier, fighter);
        fighter.AcceptedRepairRequest();
        Debug.LogWarning("Barrier Added " + _barrierRepairSystem.Count, fighter.transform);
    }

    private void CanceledRepair(BarrierStateBehaviour barrier, FighterStateBehaviour fighter)
    {
        fighter.RepairCompleteOrCanceled();
    }

    private void RepairComplete(BarrierStateBehaviour barrier)
    {
        //Listen for Barrier event as repair completed
        //Disengage Player NPC from Repair.
        if (_barrierRepairSystem.Count > 0)
        {
            var result = _barrierRepairSystem.FirstOrDefault(x => x.Key == barrier);
            if (!result.Equals(default(KeyValuePair<BarrierStateBehaviour, FighterStateBehaviour>)))
            {
                result.Value.RepairCompleteOrCanceled();
                _barrierRepairSystem.Remove(result.Key);
                Debug.LogWarning("Request to Stop Repairs and removed " + _barrierRepairSystem.Count , result.Value.transform);
                return;
            }
        }
        Debug.LogWarning("Error finding Dictionary pair " + _barrierRepairSystem.Count, transform);
    }
    private void OnDestroy()
    {
        FighterStateBehaviour.AssignThis -= (x, y) => CheckForDuplicatesThenAdd(x, y);
        BarrierStateBehaviour.FullRepairEvent -= (x) => RepairComplete(x);
    }
}
