using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField]
    public int Health { get; protected set; }

    //Component
    protected Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public virtual void takeDamage(int damage)
    {
        Health -= damage;

        if (Health < 0)
        { 
            //Play Death animation
            animator.Play("Base Layer.EnemyDie", 0);
        }
    }

    /// <summary>
    /// Removes the entity. Called from animation event on EntityDie
    /// </summary>
    protected virtual void EntityDie()
    {
        Destroy(this.gameObject);
    }
}
