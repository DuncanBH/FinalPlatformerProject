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
        Instantiate(crab, transform.position, Quaternion.identity);
        Instantiate(crab, transform.position * 1.1f, Quaternion.identity);
        Instantiate(crab, transform.position, Quaternion.identity);
        Instantiate(crab, transform.position, Quaternion.identity);
        Instantiate(crab, transform.position, Quaternion.identity);
        Instantiate(crab, transform.position, Quaternion.identity);
        Instantiate(crab, transform.position, Quaternion.identity);
        Instantiate(crab, transform.position, Quaternion.identity);
        Instantiate(crab, transform.position, Quaternion.identity);
        Instantiate(crab, transform.position, Quaternion.identity);
        Instantiate(crab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
