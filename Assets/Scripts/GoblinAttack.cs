using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : GoblinBehavior
{
   
    public float cooldown;
    private float cooldownTimer;

    private float timer;
    public float searchTime;

    private void OnEnable()
    {

        cooldownTimer = 0;

    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        timer += Time.deltaTime;

        if (cooldownTimer >= cooldown && InRange())
        {
            cooldownTimer = 0;
            Attack();
        } else GetTarget();

        if (CheckDirection()) 
        {
            goblin.enemy.rb.velocity = new Vector2(0, goblin.enemy.rb.velocity.y);
        }

    }

    private bool InRange()
    {
        if (Mathf.Abs(goblin.enemy.xDistance) <= goblin.enemy.range) return true;

        return false;
    }

    private void Attack()
    {
        goblin.enemy.animator.SetTrigger("Attack");

        RaycastHit2D hit;

        if(goblin.facingLeft) hit = Physics2D.BoxCast(goblin.enemy.capsule.bounds.center, goblin.enemy.capsule.bounds.size, 0f, Vector2.left, goblin.enemy.range, goblin.enemy.heroLayer);
        else hit = Physics2D.BoxCast(goblin.enemy.capsule.bounds.center, goblin.enemy.capsule.bounds.size, 0f, Vector2.right, goblin.enemy.range, goblin.enemy.heroLayer);

        Debug.Log(hit.collider.name);

        if (hit.collider.name == "Hero")
        {
            hit.collider.GetComponent<Player>().TakeDamage(goblin.enemy.damage);
        }
        
    }

    private void GetTarget()
    {
        if (timer >= searchTime)
        {
            timer = 0;
            if (goblin.enemy.xDistance > 0)
            {
                goblin.facingLeft = true;
                goblin.enemy.sr.flipX = true;
                goblin.enemy.rb.velocity = new Vector2 (-goblin.speed, goblin.enemy.rb.velocity.y);
            } else 
            {
                goblin.facingLeft = false;
                goblin.enemy.sr.flipX = false;
                goblin.enemy.rb.velocity = new Vector2 (goblin.speed, goblin.enemy.rb.velocity.y);
            }
        }
    }


}
