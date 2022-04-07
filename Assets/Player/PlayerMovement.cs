using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField]
    private float speedModif = 10f;
    [SerializeField]
    private float fallModif = 2f;
    [SerializeField]
    private float jumpHeight = 10f;
    [SerializeField]
    private float groundCheckDistance;
    [SerializeField]
    private float decelerateTime = 1f;

    new Transform transform;
    new Rigidbody2D rigidbody;
    new Collider2D collider;
    Animator animator;

    private int _layerMask;

    float inputX;
    float inputY;

    bool inputJump;
    bool inputAttack;

    bool attacking;
    bool jumping;
    bool facingRight;

    float slowDownTime;
    

    void Awake()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        _layerMask = ~LayerMask.GetMask("Player");
    }

    private void FixedUpdate()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        inputJump = Input.GetAxisRaw("Jump") == 1 ? true : false;
        inputAttack = Input.GetAxisRaw("Fire1") == 1 ? true : false;

        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, _layerMask);
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.red);

        //Jumping
        if (inputJump && raycastHit && !jumping)
        {
            jumping = true;
            StartCoroutine(Jump());
        }

        //Grounded anti-slip
        if (raycastHit && inputX == 0)
        {
            Vector2 target = new Vector2(0, rigidbody.velocity.y);
            rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, target, slowDownTime/decelerateTime);
            slowDownTime += Time.fixedDeltaTime;
        }
        else
        {
            slowDownTime = 0f;
        }

        rigidbody.velocity += new Vector2(inputX, 0) * speedModif * Time.deltaTime;

        //Turning around 
        if (raycastHit && facingRight && rigidbody.velocity.x < 0) 
        {
            print("turning to left");
            facingRight = false;
            transform.rotation = Quaternion.Euler(0 ,180, 0);
        }
        else if (raycastHit && !facingRight && rigidbody.velocity.x > 0)
        {
            print("turning to right");
            facingRight = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //Falling
        if (rigidbody.velocity.y < 0)
        {
            rigidbody.gravityScale = fallModif;
        }
        else
        {
            rigidbody.gravityScale = 1f;
        }

        //Attacking
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
    IEnumerator Jump()
    {
        rigidbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.75f);
        jumping = false;
    }
}
