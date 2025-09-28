using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState {FollowPath, AttackHouse}

    public EnemyState currState;
    [SerializeField] private float enemyAttackDistance = 2f;
    [SerializeField] private Transform baseTnfm; //remember to set this in editor!
    private NavMeshAgent agent;

    void Awake() {
        agent = GetComponentInParent<NavMeshAgent>();
    }
   
    void Update() {
        
        if (currState == EnemyState.FollowPath){
            FollowPath();
        }
        else {
            AttackHouse();
        }
    }

    void FollowPath() {
       
        agent.isStopped = false;
        agent.SetDestination(baseTnfm.position);

        float distToHouse = Vector3.Distance(transform.position, baseTnfm.position);

        if (distToHouse < enemyAttackDistance) {
            currState = EnemyState.AttackHouse;
        }
    }

    void AttackHouse() {
        agent.isStopped = true;
    }
}
