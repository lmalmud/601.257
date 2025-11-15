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
    [SerializeField] private WaveSpawner mySpawner; //the wave spawner that spawned this enemy in
    [SerializeField] public int checkpointIndex; //index in checkpoint array this enemy just passed
    //starts at negative one so updateCheckpoint() can be used in awake
    [SerializeField] private Transform nextCheckpoint; //the checkpoint this enemy should be headed towards

    private NavMeshAgent agent;
    [SerializeField] Animator animator;

    
    void Awake()
    {
        baseTnfm = GameObject.Find("House").transform;

        agent = GetComponentInParent<NavMeshAgent>();
    }
    
    void Start()
    {
        checkpointIndex = -1;//if i dont have this here, checkpointIndex starts at 0 for some reason
        updateCheckpoint(); //sets nextCheckpoint to the first checkpoint and checkpointIndex to 0
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
        agent.SetDestination(nextCheckpoint.position);

        float distToCheckpoint = Vector3.Distance(transform.position, baseTnfm.position);

        if (distToCheckpoint < enemyAttackDistance)
        {
            currState = EnemyState.AttackHouse;
        }
    }

    void AttackHouse()
    {
        agent.isStopped = true;
        gameObject.GetComponentInParent<SphereCollider>().enabled = false;
        animator.SetBool("IsAtEndpoint", true);
    }

    public void setSpawner(WaveSpawner s)
    {
        mySpawner = s;
    }

    public WaveSpawner getSpawner()
    {
        return mySpawner;
    }

    public void updateCheckpoint()
    {
        //when a checkpoint along the path is reached, get the next checkpoint along the path
        //then increment the checkpoint index
        nextCheckpoint = mySpawner.getNextCheckpoint(checkpointIndex);
        checkpointIndex++;

    }

    public void setNextCheckpoint(Transform next)
    {
        nextCheckpoint = next;
    }
    
    public Transform getNextCheckpoint()
    {
        return nextCheckpoint;
    }
}
