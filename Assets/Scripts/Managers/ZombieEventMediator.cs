using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class ZombieEventMediator : MonoBehaviour
{
    public static event System.Action ZombiesWin;
    public delegate void BarrierState(List<BarrierStateBehaviour> bariers);
    public static event BarrierState BarrierDestoryed;
    public static event BarrierState BarrierRepaired;
    public List<Transform> FighterTargets;
    
    public static ZombieEventMediator Instance { get; private set; }
    private Dictionary<ZombieStateBehaviour, FighterStateBehaviour> _barrierAttack;
    [SerializeField] private float _meleeDamage = 5.0f;
    private List<BarrierStateBehaviour> _brokenBarriers;

    private void Awake()
    {
        FighterTargets = new();
        if(ZombieEventMediator.Instance is null)
            Instance = this;
        if (ZombieEventMediator.Instance != this)
            Destroy(gameObject);
        FighterStateBehaviour.AddFighter += (x) => AddRemoveFighters(x);
        FighterStateBehaviour.RemoveFighter += (x) => AddRemoveFighters(x);
    }

    void Start()
    {
        // Listen for a list of hit colliders
        _brokenBarriers = new(0);
        ZombieAttackHitBoxBehaviour.HitBoxInteractables += (x) => AttackPattern(x);
        BarrierStateBehaviour.DestoryedEvent += (x) => AddBarrier(x);
        BarrierStateBehaviour.RepairEvent += (x, y) => RemoveBarrier(x);
        // if a barrier is present in the list then damage barrier first.
        // if no barrier is detected on row skip to attack players after passing the barrier line.
        //Todo: decide if an open barrier should make all zombies sworm in threw the opening with some
        // stragglers ignoring the barrier. (inteligence level)
    }

    private bool _gameOver = false;
    private void AddRemoveFighters(Transform fighter)
    {
        if (FighterTargets.Count > 0)
        {
            var temp = FighterTargets.FirstOrDefault(x => x == fighter);
            if (temp == fighter)
            {
                FighterTargets.Remove(fighter);
                if (FighterTargets.Count == 0)
                {
                    _gameOver = true;
                    ZombiesWin?.Invoke();
                }
                return;
            }
        }
        FighterTargets.Add(fighter);
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

    private void AddBarrier(BarrierStateBehaviour addBarrier)
    {
        //barrier destoryoued add to list.
        var barrier = _brokenBarriers.FirstOrDefault(x => x == addBarrier);
        if (barrier)
            return;
        _brokenBarriers.Add(addBarrier);
        BarrierDestoryed?.Invoke(_brokenBarriers);
    }

    private void RemoveBarrier(BarrierStateBehaviour removeBarrier)
    {
        var barrier = _brokenBarriers.FirstOrDefault(x => x == removeBarrier);
        if(barrier is null)
            return;
        _brokenBarriers.Remove(barrier);
        BarrierRepaired?.Invoke(_brokenBarriers);
    }
}
