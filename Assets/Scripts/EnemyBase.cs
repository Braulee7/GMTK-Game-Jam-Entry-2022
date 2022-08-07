using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Rigidbody2D rb {get; private set;}
    public SpriteRenderer sr {get; private set;}
    public Animator animator {get; private set;}
    public CapsuleCollider2D capsule {get; private set;}

    public float maxHealth;
    public float currentHealth; 
    public float damage;
    public float range;

    public float xDistance;

    public Transform target;
    public LayerMask heroLayer;

    [Header ("SFX")]
    [SerializeField] public AudioClip deathSound;
    [SerializeField] public AudioClip hurtSound;

    public int points;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider2D>();
    }

    public void ResetState()
    {
        currentHealth = maxHealth;
    }

    public void SetStats(float damage)
    {
        this.currentHealth = maxHealth;
        this.damage = damage;
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hit");

        this.currentHealth -= damage;

        

        EvaluateHealth();
        
    }

    public void EvaluateHealth()
    {
        if (this.currentHealth <= 0)
        {
            animator.SetTrigger("Death");

            AudioManager.instance.PlayOnce(deathSound);

            FindObjectOfType<GameManager>().SetScore(this.points);
            this.damage = 0;
            this.rb.isKinematic = true;

            Invoke(nameof(Death), 2f);
            Death();
        }
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }
}
