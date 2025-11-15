using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    public Life life;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void kill()
    {
        Debug.Log("kill");
        life.amount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
