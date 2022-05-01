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

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
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
        yield return new WaitForSeconds(1.5f);
        menuSystem.GameOver();
        Destroy(this.gameObject);
    }
}
