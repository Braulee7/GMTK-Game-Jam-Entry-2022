using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : EnemyBase
{
    
    public float speed;
    
    public bool facingLeft;

    [Header ("SFX")]
    [SerializeField] private AudioClip throwSound;
    



    private void Update()
    {
        if (facingLeft) sr.flipX = true;
        else sr.flipX = false;
        
        xDistance = this.transform.position.x - target.position.x;
    }

    public void MoveLeft()
    {
        facingLeft = true;

        sr.flipX = true;
        rb.velocity = new Vector2 (-speed, rb.velocity.y);
    }

    public void MoveRight()
    {
        facingLeft = false;

        sr.flipX = false;
        rb.velocity = new Vector2 (speed, rb.velocity.y);
    }

    public void StopMovement()
    {
        rb.velocity = new Vector2 (0, rb.velocity.y);
    }

}
