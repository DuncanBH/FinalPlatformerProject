using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamagable
{
    [SerializeField]
    public int Health { get; private set; }

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //takeDamage(1);
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
            //Play Death animation
            animator.SetBool("IsDead?", true);
            Destroy(this.gameObject,2);
        }
    }
}
