using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float inputX;
    float inputY;

    bool inputJump;
    bool inputAttack;

    bool attacking;

    new Transform transform;
    new Rigidbody2D rigidbody;
    new Collider2D collider;
    new Animator animator;
    
    private int _layerMask;

    [SerializeField]
    private float speedModif = 10f;
    [SerializeField]
    private float fallModif = 2f;
    [SerializeField]
    private float jumpHeight = 10f;
    [SerializeField]
    private float groundCheckDistance;

    void Awake()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        _layerMask = ~LayerMask.GetMask("Player");
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        inputJump = Input.GetAxisRaw("Jump") == 1 ? true : false;
        inputAttack = Input.GetAxisRaw("Fire1") == 1 ? true : false;

        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, _layerMask);
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.red);

        if (inputJump && raycastHit) 
        {
            rigidbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
        rigidbody.velocity += new Vector2(inputX, 0) * speedModif * Time.deltaTime;

        if (rigidbody.velocity.y < 0)
        {
            rigidbody.gravityScale = fallModif;
        }
        else
        {
            rigidbody.gravityScale = 1f;
        }

        if (inputAttack && !attacking)
        {
            attacking = true;
            animator.SetBool("IsAttacking?", true);
            StartCoroutine(Attack());
        }
        else
        {
            animator.SetBool("IsAttacking?", false);
        }


    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.05f);
        attacking = false;
    }
}
