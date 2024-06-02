using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieBehavior : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    [SerializeField] private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        FighterStateBehaviour.RemoveFighter += (x) => TargetKilled(x);
        _targetPlayers = new();
        _rigidbody = GetComponent<Rigidbody>();
        ZombieEventMediator.BarrierDestoryed += (x) => MoveToBarrier(x);
        ZombieEventMediator.BarrierRepaired += (_) => MoveToBarrier();
        //ZombieEventMediator.BarrierRepaired
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }
    private Transform _currentBarrierTarget = null;
    private void MoveToBarrier(List<BarrierStateBehaviour> barriers = null)
    {
        if (barriers.Count <= 0)
        {
            _currentBarrierTarget = null;
            return;
        }
        int random = Random.Range(0, barriers.Count - 1);
        _currentBarrierTarget = barriers[random].transform;
    }

    private void TargetKilled(Transform fighter)
    {
        if(_target is null)
            return;
        if (_target == fighter)
        {
            _target = null;
            //Select a new player who is close
        }
    }
    private List<Transform> _targetPlayers;
    private Transform _target;
    private void MoveForward()
    {
        if (_currentBarrierTarget && transform.position.z > 2.7f)
        {
            Vector3 direction = (_currentBarrierTarget.position - transform.position).normalized;
            Vector3 moveStep = direction * moveSpeed * Time.deltaTime;

            // Move the Rigidbody
            _rigidbody.MovePosition(_rigidbody.position + moveStep);
            return;
        }
        if (_currentBarrierTarget && transform.position.z < 2.2f)
        {
            //pick a player and attack it
            //If that player is killed then target the next closest one.
            if (_targetPlayers.Count <= 0)
            {
                SelectPlayer();
                return;
            }
            if (_target is null)
            {
                SelectPlayer();
                return;
            }
            //Look at player
            Vector3 direction = (_target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = lookRotation;
        }
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void SelectPlayer()
    {
        _targetPlayers = ZombieEventMediator.Instance.FighterTargets;
        if (_targetPlayers.Count <= 0) return;
        _target = _targetPlayers
            .OrderBy(player => Vector3.Distance(transform.position, player.position))
            .FirstOrDefault();
    }
}
