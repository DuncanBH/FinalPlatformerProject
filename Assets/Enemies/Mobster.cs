using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Mobster : Enemy
{
    //Control Varibales
    [SerializeField]
    private bool DoPatrol = false;
    [SerializeField]
    private float timeBetweenShots;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float leftX = 5;
    [SerializeField]
    private float rightX = 5;
    [SerializeField]
    private float viewDistance = 10;
    [SerializeField]
    private bool isFacingRight = true;
    [SerializeField]
    public AudioClip gunShotSound;

    //Object References
    [SerializeField]
    GameObject bullet;

    //Components
    Rigidbody2D rb2d;
    new Transform transform;
    AudioSource audioSource;

    //Internal variables
    bool _found = false;
    int _layerMask;
    int _playerLayer;
    private float _shootTimer = 0.0f;
    private float leftTarget;
    private float rightTarget;


    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();

        _layerMask = LayerMask.GetMask("Player") | LayerMask.GetMask("Ground");
        _playerLayer = LayerMask.NameToLayer("Player");

        leftTarget = transform.position.x - leftX;
        rightTarget = transform.position.x + rightX;
    }

    private void Update()
    {
        //Search until player is found, then shoot
        Vector3 directionVector = new Vector3(isFacingRight ? 1 : -1, 0, 0);

        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, directionVector, viewDistance, _layerMask);
        Debug.DrawRay(transform.position, directionVector * viewDistance, Color.blue);
        //Debug.DrawRay(transform.position, directionVector * viewDistance, Color.blue);

        if (raycastHit)
        {
            if (raycastHit.collider.gameObject.layer == _playerLayer)
            {
                animator.SetBool("IsShooting?", true);
                _found = true;

                if (_shootTimer > timeBetweenShots)
                {
                    if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.EnemyDie"))
                    {
                        Bullet bullet = Instantiate(this.bullet, transform.position, transform.rotation).GetComponent<Bullet>();
                        bullet.direction = Vector2.right;
                        _shootTimer = 0.0f;
                        audioSource.PlayOneShot(gunShotSound);
                    } 
                }
                _shootTimer += Time.deltaTime;
            }
            else
            {
                _shootTimer = 0.0f;
                _found = false;
                animator.SetBool("IsShooting?", false);
            }
        }
        else
        {
            _found = false;
            animator.SetBool("IsShooting?", false);
        }
    }

    private void FixedUpdate()
    {
        if (!_found & DoPatrol)
        {
            if (isFacingRight && transform.position.x > rightTarget)
            {
                isFacingRight = false;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (!isFacingRight && transform.position.x < leftTarget)
            {
                isFacingRight = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            rb2d.velocity = new Vector2(isFacingRight ? 1 : -1 * speed, Physics2D.gravity.y);
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
        animator.SetFloat("AbsEnemyVelocity", Mathf.Abs(rb2d.velocity.x));
    }

}
