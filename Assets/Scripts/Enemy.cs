using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.addEnemy(this);
    }

    void OnDestroy()
    {
        GameManager.instance.removeEnemy(this);
    }

}
