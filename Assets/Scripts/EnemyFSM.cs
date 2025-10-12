using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
    State Machine for the enemy behavior.
    Two states as of 10/11: FollowPath and AttackHouse
    uses a NavMeshAgent on the path to track the base and travel to it.
*/

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState { FollowPath, AttackHouse }

    public EnemyState currState;

    [Header("Enemy FSM parameters")]
    [SerializeField] private float enemyAttackDistance = 2f;
    [SerializeField] private Transform baseTnfm;
    private NavMeshAgent agent;

    void Awake()
    {
        baseTnfm = GameObject.Find("House").transform;
        agent = GetComponentInParent<NavMeshAgent>();
    }

    void Update()
    {

        if (currState == EnemyState.FollowPath)
        {
            FollowPath();
        }
        else
        {
            AttackHouse();
        }
    }

    void FollowPath()
    {

        agent.isStopped = false;
        agent.SetDestination(baseTnfm.position);

        float distToHouse = Vector3.Distance(transform.position, baseTnfm.position);

        if (distToHouse < enemyAttackDistance)
        {
            currState = EnemyState.AttackHouse;
        }
    }

    void AttackHouse()
    {
        agent.isStopped = true;
    }
}
