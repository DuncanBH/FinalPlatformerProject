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
            //Play Death animation
            animator.Play("Base Layer.EnemyDie", 0);
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    /// <summary>
    /// Removes the entity. Called from animation event on EntityDie
    /// </summary>
    private void EntityDie()
    {
        Destroy(this.gameObject);
    }
}
