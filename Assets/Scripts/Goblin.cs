using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{

    public float speed;
    public bool facingLeft;
    
    public GoblinPatrol patrol {get; private set;}
    public GoblinAttack attack {get; private set;}
    public EnemyBase enemy {get; private set;}

    [Header ("SFX")]
    [SerializeField] private AudioClip attackSound;

    private void Start()
    {
        attack = GetComponent<GoblinAttack>();
        enemy = GetComponent<EnemyBase>();
        enemy.ResetState();

        attack.Enable();
    }

    private void Update()
    {
        enemy.xDistance = transform.position.x - enemy.target.position.x;
    } 

    public void SetVelocity(float direction = 1)
    {
        speed *= direction;
        if(enemy.rb != null){
            enemy.rb.velocity = new Vector2 (speed, enemy.rb.velocity.y);
            
            if (speed < 0)
            {
                enemy.sr.flipX = true;
                facingLeft = true;
            }
            else 
            {
                enemy.sr.flipX = false;
                facingLeft = false;
            }
        }
    }

    public  void TakeDamage (float damage)  
    {
        enemy.animator.SetTrigger("Hit");

        AudioManager.instance.PlayOnce(enemy.hurtSound);

        enemy.currentHealth -= damage;

        enemy.EvaluateHealth();

    }

    

}
