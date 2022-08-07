using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberBehavior : MonoBehaviour
{
    public Bomber bomber {get; private set;}

    public GameObject bombHolding;
    public Transform bombSpawn {get; private set;}
    public FollowBehaviour follow {get; private set;}

    public LayerMask obstacleLayer;
    
    private void Awake()
    {
        bomber = GetComponent<Bomber>();
        bombSpawn = bombHolding.GetComponent<Transform>();
        follow = bombHolding.GetComponent<FollowBehaviour>();
    }

    public virtual void Enable()
    {
        this.enabled = true;
    }

    public virtual void Disable()
    {
        this.enabled = false;
    }

    public bool CheckDirection()
    {
        if (bomber.facingLeft) follow.x = -Mathf.Abs(follow.x);
        else follow.x = Mathf.Abs(follow.x);

        RaycastHit2D below = Physics2D.Raycast(bombSpawn.position, Vector2.down, 1.0f, obstacleLayer);
        RaycastHit2D forward;

        if (bomber.facingLeft) forward = Physics2D.Raycast(bombSpawn.position, Vector2.left, 1.0f, obstacleLayer);
        else forward = Physics2D.Raycast(bombSpawn.position, Vector2.right, 1.0f, obstacleLayer);

        if (below.collider == null || forward.collider != null) return true;
        return false;
    }


}
