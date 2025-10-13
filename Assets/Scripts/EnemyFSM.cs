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
    [SerializeField] private int checkpointIndex = -1; //index in checkpoint array this enemy just passed
    //starts at negative one so updateCheckpoint() can be used in awake
    [SerializeField] private Transform nextCheckpoint; //the checkpoint this enemy should be headed towards

    private NavMeshAgent agent;

    void Awake()
    {
        print("index in Awake: " + checkpointIndex);
        baseTnfm = GameObject.Find("House").transform;

        agent = GetComponentInParent<NavMeshAgent>();
    }
    
    void Start()
    {
        checkpointIndex = -1;//if i dont have this here, checkpointIndex starts at 0 for some reason
        print("index before start: " + checkpointIndex);
        updateCheckpoint(); //sets nextCheckpoint to the first checkpoint and checkpointIndex to 0
        print("index after start: " + checkpointIndex);
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
    }

    public void setSpawner(WaveSpawner s)
    {
        mySpawner = s;
    }

    void OnTriggerEnter(Collider other)
    {
        print("trigger entered chkpt");
        if (other.tag == "Checkpoint")
        {
            print("hit checkpoint");
            updateCheckpoint();
        }
    }
    
    private void updateCheckpoint()
    {
        //when a checkpoint along the path is reached, get the next checkpoint along the path
        //then increment the checkpoint index
        nextCheckpoint = mySpawner.getNextCheckpoint(checkpointIndex);
        checkpointIndex++;
        
    }
}
