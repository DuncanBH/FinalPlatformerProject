using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField]
    public int Health { get; private set; }
    public float timeBetweenShots;
    public float speed;
    public float leftX;
    public float rightX;



    public bool patrolling = true;
    public float viewDistance;

    [SerializeField]
    GameObject bullet;
    Animator animator;
    Rigidbody2D rb2d;
    


    

    //internal variables
    int direction = 1;
    Vector2 enemyVelocity;
    bool found = false;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemyVelocity = Vector2.zero;
        
    }
    private void Update()
    {
        //stop patroling and shoot if player found
        if (!found)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(direction, 0, 0), new Vector3(direction * viewDistance, 0, 0));
            GameObject[] blocks = GameObject.FindGameObjectsWithTag("Ground");
            Debug.DrawRay(transform.position + new Vector3(direction, 0, 0), new Vector3(direction * viewDistance, 0, 0), Color.blue);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.tag == "Ground")
                {
                    break;
                }
                if (hits[i].collider.gameObject.tag == "Player")
                {
                    patrolling = false;
                    found = true;
                    StartCoroutine(ShootBullet());
                }

            }
        }
    }
    private void FixedUpdate()
    {
        if (patrolling)
        {
            if(transform.position.x <= leftX)
            {
                direction = 1;
            }
            if(transform.position.x >= rightX)
            {
                direction = -1;
            }
            enemyVelocity.x = speed*direction;

        }
        else
        {
            enemyVelocity = Vector2.zero;
        }
        rb2d.velocity = enemyVelocity;
    }

    public void takeDamage(int damage)
    {
        Health -= damage;

        if (Health < 0)
        {
            StopCoroutine(ShootBullet());
            //Play Death animation
            animator.Play("Base Layer.EnemyDie", 0);
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Removes the entity. Called from animation event on EntityDie
    /// </summary>
    private void EntityDie()
    {
        Destroy(this.gameObject);
    }

    IEnumerator ShootBullet()
    {
        Bullet tempBulletScript = Instantiate(bullet, transform.position,transform.rotation).GetComponent<Bullet>();
        tempBulletScript.direction = Vector2.left;
        yield return new WaitForSeconds(timeBetweenShots);
        StartCoroutine(ShootBullet());
    }

}
