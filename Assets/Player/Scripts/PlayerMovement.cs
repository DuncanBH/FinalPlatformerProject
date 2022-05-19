using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float NORMAL_GRAVITY = 1f;

    //Editor fields
    [SerializeField]
    private float speedModif = 10f;
    [SerializeField]
    private float fallModif = 2f;
    [SerializeField]
    private float initialJumpPower = 2f;
    [SerializeField]
    private float jumpTimeMin = 0.75f;
    [SerializeField]
    private float groundCheckDistance;
    [SerializeField]
    private float decelerationTime = 1f;

    //Components
    new Transform transform;
    new Rigidbody2D rigidbody;
    new Collider2D collider;
    Animator animator;

    //Analog inputs
    float inputX;
    float inputY;

    //Digital Inputs
    bool inputJump;
    bool inputAttack;

    //Player States
    public bool attacking;
    bool jumping;
    bool facingRight = true;

    //Internal Variables
    private int _layerMask;
    private bool _isGrounded;
    private float _slowDownTimer = 0.0f;
    private float _jumpTime = 0.0f;

    void Awake()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        _layerMask = ~(LayerMask.GetMask("Player") | LayerMask.GetMask("CameraBounds")) ;
    }
    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        inputJump = Input.GetAxisRaw("Jump") == 1 ? true : false;
        inputAttack = Input.GetAxisRaw("Fire1") == 1 ? true : false;

        animator.SetBool("IsJumping?", !_isGrounded);

    }
    private void FixedUpdate()
    {
        //Groundcheck raycast
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, _layerMask);
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.red);

        _isGrounded = raycastHit;

        //Jumping
        if (inputJump && !jumping && _isGrounded)
        {
            jumping = true;
            rigidbody.AddForce(Vector2.up * initialJumpPower, ForceMode2D.Impulse);
            _jumpTime = 0.0f;
            //print("launch");
        }
        else if (jumping) {
            if (inputJump && _jumpTime < jumpTimeMin)
            {
                rigidbody.gravityScale = NORMAL_GRAVITY;
                //print("uppity");
            }
            else
            {
                rigidbody.gravityScale = fallModif;
                //print("downity");
            }
            
            if (_isGrounded && _jumpTime > jumpTimeMin ) { 
                jumping = false; 
            }
            _jumpTime += Time.fixedDeltaTime;
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
            rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, target, _slowDownTimer / decelerationTime);
            _slowDownTimer += Time.fixedDeltaTime;

            //"Close Enough" slowdown fudging
            if (Mathf.Abs(rigidbody.velocity.x) < 0.001f)
            {
                rigidbody.velocity = target;
            }
        }
        else
        {
            _slowDownTimer = 0f;
        }

        //Apply movement
        if (Mathf.Abs(rigidbody.velocity.x) <= speedModif)
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
        if (!jumping)
        {
            if (rigidbody.velocity.y < 0)
            {
                rigidbody.gravityScale = fallModif;
            }
            else
            {
                rigidbody.gravityScale = NORMAL_GRAVITY;
            }
        }

        //Attacking
        if (inputAttack && !attacking)
        {
            attacking = true;
            animator.SetBool("IsAttacking?", true);
        }
        else 
        {
            animator.SetBool("IsAttacking?", false);
        }
    }
}
