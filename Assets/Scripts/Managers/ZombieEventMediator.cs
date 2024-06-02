using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class ZombieEventMediator : MonoBehaviour
{
    public static ZombieEventMediator Instance { get; private set; }
    private Dictionary<ZombieStateBehaviour, FighterStateBehaviour> _barrierAttack;
    [SerializeField] private float _meleeDamage = 5.0f;
    private List<BarrierStateBehaviour> _brokenBarriers;

    private void Awake()
    {
        if(ZombieEventMediator.Instance is null)
            Instance = this;
        if (ZombieEventMediator.Instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        // Listen for a list of hit colliders
        _brokenBarriers = new(0);
        ZombieAttackHitBoxBehaviour.HitBoxInteractables += (x) => AttackPattern(x);
        BarrierStateBehaviour.DestoryedEvent += (x) => BarrierDestroyed(x);
        BarrierStateBehaviour.RepairEvent += (x, y) => BarrierRepaired(x);
        // if a barrier is present in the list then damage barrier first.
        // if no barrier is detected on row skip to attack players after passing the barrier line.
        //Todo: decide if an open barrier should make all zombies sworm in threw the opening with some
        // stragglers ignoring the barrier. (inteligence level)
    }

    private void AttackPattern(List<IDamageable> allInteractables)
    {
        //Find barrier first do damage then clear list.
        //BarrierStateBehaviour
        var barrierInteractable = allInteractables.FirstOrDefault(x => x is BarrierStateBehaviour) as BarrierStateBehaviour;
        if (barrierInteractable)
        {
            barrierInteractable.TakeDamage(_meleeDamage);
            return;
        }
        else if (barrierInteractable is null)
        {
            var fighterInteractable = allInteractables.FirstOrDefault(x => x is FighterStateBehaviour) as FighterStateBehaviour;
            if (fighterInteractable is null) return;
            fighterInteractable.TakeDamage();
        }
        //FighterStateBehaviour
        
        //ZombieStateBehaviour
    }

    private void BarrierDestroyed(BarrierStateBehaviour addBarrier)
    {
        //barrier destoryoued add to list.
        var barrier = _brokenBarriers.FirstOrDefault(x => x == addBarrier);
        if (barrier)
            return;
        
        _brokenBarriers.Add(addBarrier);
    }

    private void BarrierRepaired(BarrierStateBehaviour removeBarrier)
    {
        var barrier = _brokenBarriers.FirstOrDefault(x => x == removeBarrier);
        
    }
}
