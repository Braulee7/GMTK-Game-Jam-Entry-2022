using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinPatrol : GoblinBehavior
{
    //bool to know what direction we are moving
    private bool movingLeft;
    

    private void OnEnable()
    {
        Debug.Log("Starting Patrol");
        
        MoveRight();
    }


    private void Update()
    {
        //check if the next direction is valid
        if (CheckDirection())
        { 
            //if not switch the direction
            SwitchDirection();
        }
    }

    // public bool CheckDirection()
    // {
    //     //check if theres ground below you
    //     RaycastHit2D below = Physics2D.BoxCast(nextCapsule.bounds.center, nextCapsule.bounds.size, 0f, Vector2.down, 1.0f, obstacleLayer);
    //     RaycastHit2D forward;

    //     //check if hitting a wall
    //     if (!movingLeft) forward = Physics2D.Raycast (nextCapsule.bounds.center, Vector2.right, 1.0f, obstacleLayer);
    //     else
    //     {
    //         following.x = -following.x;
    //         forward = Physics2D.Raycast (nextCapsule.bounds.center, Vector2.left, 1.0f, obstacleLayer);
    //     }

    //     //if the colliders don't correspond to a valid entry then return true
    //     if (below.collider == null || forward.collider != null) return true;
         
    //     else return false;

    // }

    private void MoveRight()
    {

        this.goblin.SetVelocity(1);
        movingLeft = false;
    }

    private void MoveLeft()
    {

        this.goblin.SetVelocity(-1);
        movingLeft = true;
    }

    private void SwitchDirection()
    {
        if (!movingLeft)
        {
            MoveLeft();
        } else 
        {
            MoveRight();
        }
    }
}
