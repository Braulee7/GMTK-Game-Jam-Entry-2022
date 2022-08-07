using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //components
    public Rigidbody2D rb {get; private set;}
    public CircleCollider2D circle {get; private set;}
    public BoxCollider2D box {get; private set;}
    public Animator animator {get; private set;}
    public LayerMask heroLayer;
    public LayerMask obstacleLayer;

    //Combat
    public float damage;

    //AI
    public float explosionTime;
    private float timer;
    private bool exploded;

    [Header ("SFX")]
    public AudioClip explosionSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        timer = 0;
        exploded = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= explosionTime && !exploded)
        {
            Explode();
        }

        if (exploded)
        {
            Invoke(nameof(Destroy), 3.0f);
        }

        if (GroundCheck()) animator.SetTrigger("Ground");
    }

    private void Explode()
    {
        animator.SetTrigger("Explosion");

        AudioManager.instance.PlayOnce(explosionSound);

        exploded = true;

        RaycastHit2D radius = Physics2D.BoxCast(circle.bounds.center, new Vector2 (2.072042f, 1.457627f), 0f, Vector2.right, 1.0f, heroLayer);

        if (radius.collider != null)
        {
            radius.collider.GetComponent<Player>().TakeDamage(damage);
        }
    }

    private bool GroundCheck()
    {
        RaycastHit2D hit = Physics2D.BoxCast(circle.bounds.center, circle.bounds.size, 0f, Vector2.down, 1.0f, obstacleLayer);
        
        return hit.collider != null;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void Destroy()
    {
        this.gameObject.SetActive(false);
    }
}
