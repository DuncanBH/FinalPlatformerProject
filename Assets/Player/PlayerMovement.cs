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
    private float decelerationTime = 1f;

    new Transform transform;
    new Rigidbody2D rigidbody;
    new Collider2D collider;
    Animator animator;

    private int _layerMask;
    private bool _isGrounded;
    public bool IsGrounded { get { return _isGrounded; } }

    float inputX;
    float inputY;

    bool inputJump;
    bool inputAttack;

    bool attacking;
    bool jumping;
    bool facingRight = true;

    float slowDownTimer;
    

    void Awake()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        _layerMask = ~(LayerMask.GetMask("Player") | LayerMask.GetMask("CameraBounds")) ;
    }

    private void FixedUpdate()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        inputJump = Input.GetAxisRaw("Jump") == 1 ? true : false;
        inputAttack = Input.GetAxisRaw("Fire1") == 1 ? true : false;

        //Groundcheck raycast
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, _layerMask);
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.red);

        _isGrounded = raycastHit;


        //Jumping
        if (inputJump && _isGrounded && !jumping)
        {
            jumping = true;
            StartCoroutine(Jump());
        }

        //Grounded anti-slip
        if (_isGrounded && 
                (inputX == 0 //if input released
                || (rigidbody.velocity.x > 0 && inputX < 0) //if moving right but holding left
                || (rigidbody.velocity.x < 0 && inputX > 0) //if moving left but holding right
                )
            )
        {
            Vector2 target = new Vector2(0f, rigidbody.velocity.y);
            rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, target, slowDownTimer / decelerationTime);
            slowDownTimer += Time.fixedDeltaTime;

            //"Close Enough" slowdown fudging
            if (Mathf.Abs(rigidbody.velocity.x) < 0.001f)
            {
                rigidbody.velocity = target;
            }
        }
        else
        {
            slowDownTimer = 0f;
        }

        //Apply movement
        if ( Mathf.Abs(rigidbody.velocity.x) <= speedModif)
        {
            rigidbody.velocity += new Vector2(inputX * speedModif, 0) * Time.deltaTime;
        }

        //Send current velocity to animator
        animator.SetFloat("AbsPlayerVelocityX", Mathf.Abs(rigidbody.velocity.x));

        float VelX = rigidbody.velocity.x;
        //Turning around 
        if (!facingRight && _isGrounded && VelX > 0)
        {
            facingRight = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }else if (facingRight && _isGrounded && VelX < 0) 
        {
            facingRight = false;
            transform.rotation = Quaternion.Euler(0 ,180, 0);
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
