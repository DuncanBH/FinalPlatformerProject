using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : Enemy
{
    [SerializeField]
    private GameObject crab;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Removes the entity. Called from animation event on EntityDie
    /// </summary>
    protected override void EntityDie()
    {
        Vector3 position = this.transform.position + new Vector3(0, 3, 0);
        for (int i = 0; i < 10; i++ )
        {
            Instantiate(crab, position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
}
