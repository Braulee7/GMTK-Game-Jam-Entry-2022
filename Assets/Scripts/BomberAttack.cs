using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAttack : BomberBehavior
{
    //bomb
    public GameObject bomb;
    public float launchVelocity;

    //movement
    private float xAxis;
    
    //AI
    private bool bombThrown;
    public float maxDistance;
    private bool running;

    //timers
    public float cooldown;
    private float cooldownTimer;
    public float runT;
    private float runTimer;

    private void OnEnable()
    {
        cooldownTimer = Mathf.Infinity;
        runTimer = Mathf.Infinity;
    }

    private void Update()
    {
        if (CheckDirection() && running)
        {
            RunAway();
        } else 
        {
            if (CheckDirection()) bomber.StopMovement();
        }


        //check direction the bomber is facing
        if (bomber.facingLeft)
        {
            follow.x = -Mathf.Abs(follow.x);
        }
        else 
        {
            follow.x = Mathf.Abs(follow.x);
        }

        //cool down from throwing bomb
        if (bombThrown)
        {
            runTimer += Time.deltaTime;
        } else 
        {
            cooldownTimer += Time.deltaTime;
        }

        //if ready to attack
        if (cooldownTimer >= cooldown)
        {
            //reset timers
            cooldownTimer = 0;
            runTimer = 0;
           
            //throw bomb
            ThrowBomb();
        }

        //run away from the hero
        if (runTimer >= runT)
        {
            runTimer = 0;
            bombThrown = false;
            Run();
        }
        


    }

    private void Run()
    {

        //what direction the hero is in
        if (bomber.xDistance > 0) bomber.facingLeft = true;
        else bomber.facingLeft = false;

        //send a boxcast to check if hero is within range
        if (InRange())
        {
            Debug.Log("Hero is too close need To run");
            RunAway();
        } else
        {
            Debug.Log("Hero is far enough ready to throw another bomb");
            running = false;
            bomber.StopMovement();
        }

    }

    private bool InRange()
    {
        RaycastHit2D box;

        //send box cast to direction of the hero
        if (bomber.facingLeft) box = Physics2D.BoxCast(bomber.capsule.bounds.center, bomber.capsule.bounds.size, 0f, Vector2.left, bomber.range - 2f, bomber.heroLayer);
        else box = Physics2D.BoxCast(bomber.capsule.bounds.center, bomber.capsule.bounds.size, 0f, Vector2.right, bomber.range - 2f, bomber.heroLayer);

        //if cast hits then hero is in range
        if (box.collider != null) return true;
        
        
        return false;
    
    }

    private void RunAway()
    {
        //let computer know bomber is running
        running = true;

        //check what direction bomber needs to run away from
        if (bomber.facingLeft)
        {
            //run left
            bomber.MoveRight();
        } else
        {
            //move right
            bomber.MoveLeft();
        }

    }

    private void ThrowBomb()
    {
        bombThrown = true;

        GameObject copyBomb = Instantiate(bomb, bombSpawn.position, Quaternion.identity);

        //set damage of instantiates bomb to the bomber's damage
        copyBomb.GetComponent<Bomb>().SetDamage(bomber.damage);

        //launch the bomb
        if (bomber.facingLeft) copyBomb.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(-launchVelocity, launchVelocity, 0));
        else copyBomb.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(launchVelocity, launchVelocity, 0));

    }
}
