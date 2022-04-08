using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField]
    public int Health { get; private set; }
    
    Animator animator;

    public void takeDamage(int damage)
    {
        Health -= damage;

        if (Health < 0)
        {
            animator.Play("Base Layer.EnemyDie", 0);
            //entityDie();
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void entityDie()
    {
        Destroy(this.gameObject);
    }
}
