using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //components
    public Rigidbody2D rb {get; private set;}
    public Animator animator {get; private set;}
    public SpriteRenderer sr {get; private set;}
    public CapsuleCollider2D capsule {get; private set;}
    
    //movement variables
    private bool isGrounded;
    private bool jump;
    private bool doubleJump;
    private bool isJumping;
    private float xAxis;

    [Header ("Movement Variables")]
    public float jumpForce;
    public float speed;

    public float dashSpeed;
    private bool dash;
    public float dashTimer {get; private set;}
    public float dashCooldown;

    //combat variables
    [Header ("Combat Variables")]
    
    public float maxHealth;
    public float damage;
    private float range;
    private bool attack;
    private bool facingLeft;
    public float currentHealth {get; private set;}

    //layermasks
    [Header ("Layer Masks")]
    public LayerMask obstacleLayer;
    public LayerMask enemyLayer;

    [Header ("SFX")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip attackSounds;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip healSound;

    //checkpoint
    private Transform checkpoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        capsule = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        //update the dash timer
        dashTimer += Time.deltaTime;


        //check the horizontal movement
        xAxis = Input.GetAxisRaw("Horizontal");

        //check if jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GroundCheck())
            {
                jump = true;
            } else if (isJumping)
            {
                doubleJump = true;
            }
        }

        //check if attacking
        if (Input.GetMouseButtonDown(0))
        {
            attack = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (dashTimer >= dashCooldown)
            {
                dashTimer = 0;
                dash = true;
                Debug.Log("Dashing");
            }
            
        }


    }

    private void FixedUpdate()
    {
        //moving right
        if (xAxis > 0)
        {
            facingLeft = false;
            sr.flipX = false;
            rb.velocity = new Vector2 (speed, rb.velocity.y);
        } 

        //moving left
        if (xAxis < 0)
        {
            facingLeft = true;
            sr.flipX = true;
            rb.velocity = new Vector2 (-speed, rb.velocity.y);
        }

        //moving in any direction
        if (xAxis != 0)
        {
            animator.SetBool("Run", true);
        } 

        //not moving at all
        else 
        {
            animator.SetBool ("Run", false);
            rb.velocity = new Vector2 (0, rb.velocity.y);
        }

        //jumping
        if (jump)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
            jump = false;
            isJumping = true;
        }

        if (doubleJump)
        {
            animator.SetTrigger("Double Jump");
            rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
            isJumping = false;
            jump = false;
            doubleJump = false;
        }

        if (attack && GroundCheck())
        {
            attack = false;
            Attack();
        }

        if (dash)
        {
            animator.SetTrigger("Dash");
            dash = false;

            rb.velocity = transform.right * (int)xAxis * dashSpeed;
            
        }
    }

    private bool GroundCheck()
    {
        RaycastHit2D hit = Physics2D.BoxCast(capsule.bounds.center, new Vector2(capsule.bounds.size.x - 0.25f, capsule.bounds.size.y), 0f, Vector2.down, 0.25f, obstacleLayer);
        return hit.collider != null;
    }

    public void TakeDamage(float damage)
    {
        //play animation
        animator.SetTrigger("Hit");
        
        //play sound 
        AudioManager.instance.PlayOnce(hurtSound);

        //calculate health
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            //PlaySounds
            AudioManager.instance.PlayOnce(deathSound);
            animator.SetBool("Death", true);
            Invoke(nameof(ResetState), 2.0f);
        }
    }

    public void Heal(float health)
    {
        //increase health
        currentHealth += health;

        //ensure health isn't greater than the max
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        //play sound
        AudioManager.instance.PlayOnce(healSound);
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

        AudioManager.instance.PlayOnce(attackSounds);

        RaycastHit2D hit;

        if (facingLeft) hit = Physics2D.BoxCast(capsule.bounds.center, new Vector2(capsule.bounds.size.x, capsule.bounds.size.y - 0.2f), 0f, Vector2.left, range, enemyLayer);
        else hit = Physics2D.BoxCast(capsule.bounds.center, new Vector2(capsule.bounds.size.x, capsule.bounds.size.y - 0.2f), 0f, Vector2.right, range, enemyLayer);


        Debug.Log(hit.collider);

        if (hit.collider != null)
        {
            EnemyBase enemy = hit.collider.gameObject.GetComponent<EnemyBase>();

            if (enemy != null) enemy.TakeDamage(damage);
            else 
            {
                Debug.Log("No Enemy component found");
                Goblin goblin = hit.collider.gameObject.GetComponent<Goblin>();

                if (goblin != null) goblin.TakeDamage(damage);
                else Debug.Log("No Goblin Component found");
            }
        }


    }

    private void ResetState()
    {
        animator.SetBool ("Death", false);

        currentHealth = maxHealth;
        SetStats();
        range = 2;

        dashTimer = Mathf.Infinity;

        this.transform.position = checkpoint.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Checkpoint")
        {
            checkpoint = other.gameObject.GetComponent<Transform>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Harmful")
        {
            TakeDamage(maxHealth);
        }
    }

    public void SetStats (float damage = 2)
    {
        this.damage = damage;
    }
}
