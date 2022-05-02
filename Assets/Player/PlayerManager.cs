using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamagable
{
    [SerializeField]
    public int Health { get; private set; }

    [SerializeField]
    PauseMenus menuSystem;
    
    Animator animator;
    PlayerMovement playerMovement;
    Rigidbody2D rb2d;
    BoxCollider2D boxCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    public void takeDamage(int damage)
    {
        Health -= damage;

        if (Health < 0)
        {
            StartCoroutine(gameOverSequence());
        }
    }

    public IEnumerator gameOverSequence()
    {
        animator.SetBool("IsDead?", true);
        playerMovement.enabled = false;
        rb2d.velocity = Vector2.zero;
        yield return new WaitForSeconds(1.5f);
        rb2d.isKinematic = true;
        boxCollider.enabled = false;
        menuSystem.GameOver();
        
    }
}
