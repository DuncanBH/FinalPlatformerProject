using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField]
    public int Health { get; private set; }
    public void takeDamage(int damage)
    {
        Health -= damage;

        if (Health < 0)
        {
            entityDie();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void entityDie()
    {
        Destroy(this.gameObject);
    }
}
