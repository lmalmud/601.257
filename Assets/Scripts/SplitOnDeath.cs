using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitOnDeath : MonoBehaviour
{
    //public bool isChild = false;
    [SerializeField] private Life life;
    [SerializeField] private GameObject childPrefab;

    void Awake()
    {
        life = GetComponent<Life>();
        life.onDeath.AddListener(SplitUp);
    }
    
    void SplitUp()
    {
        
        GameObject childObj1 = Instantiate(childPrefab, transform.position, transform.rotation);
        GameObject childObj2 = Instantiate(childPrefab, transform.position, transform.rotation);

        EnemyFSM e1 = childObj1.GetComponentInChildren<EnemyFSM>();
        EnemyFSM e2 = childObj2.GetComponentInChildren<EnemyFSM>();

        //children still need to be associated to a spawner!
        e1.setSpawner( this.GetComponentInChildren<EnemyFSM>().getSpawner()) ;
        e2.setSpawner(this.GetComponentInChildren<EnemyFSM>().getSpawner());

        
        
        //TODO they still go backwards! fix it!!
        //otherwise they go backwards to the first checkpoint
        e1.setNextCheckpoint(this.GetComponentInChildren<EnemyFSM>().getNextCheckpoint());
        e2.setNextCheckpoint(this.GetComponentInChildren<EnemyFSM>().getNextCheckpoint());

        //e1.updateCheckpoint();
        //e2.updateCheckpoint();
    }
}
