using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieEventMediator : MonoBehaviour
{
    private Dictionary<ZombieStateBehaviour, FighterStateBehaviour> _barrierAttack;
    [SerializeField] private float _meleeDamage = 5.0f;
    void Start()
    {
        // Listen for a list of hit colliders
        ZombieAttackHitBoxBehaviour.HitBoxInteractables += (x) => AttackPattern(x);
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
}
