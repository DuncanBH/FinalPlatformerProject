using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Editor fields
    [SerializeField]
    private float speedModif = 10f;
    [SerializeField]
    private float fallModif = 2f;
    [SerializeField]
    private float jumpPower = 2f;
    [SerializeField]
    private float jumpTimeMax = 0.75f;
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
    bool attacking;
    bool jumping;
    bool facingRight = true;

    //Internal Variables
    private int _layerMask;
    private bool _isGrounded;
    public bool IsGrounded { get { return _isGrounded; } }

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
            _jumpTime = 0.0f;
            StartCoroutine(Jump());
        } else if (jumping){
            _jumpTime += Time.fixedDeltaTime;
            StartCoroutine(Jump());
        }
        Debug.Log(_jumpTime);

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
        do
        {
            float jumpForce = Mathf.Lerp( 0, jumpPower, jumpTimeMax - _jumpTime);
            print(jumpForce);
            rigidbody.AddForce(Vector2.up * jumpForce);
            yield return null;
        } while ((inputJump && _jumpTime < jumpTimeMax)|| _jumpTime < jumpTimeMin) ;

        jumping = false;
    }
}
