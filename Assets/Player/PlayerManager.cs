using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamagable
{
    [SerializeField]
    public int Health { get; private set; }

    Animator animator;

    [SerializeField]
    GameObject menuSystem;

    PauseMenus menuSystemScript;
    PlayerMovement playerMovement;
    new Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        menuSystemScript = menuSystem.GetComponent<PauseMenus>();
        playerMovement = GetComponent<PlayerMovement>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        rigidbody.Sleep();
        yield return new WaitForSeconds(2.0f);
        menuSystemScript.GameOver();
        Destroy(this.gameObject);
    }
}
