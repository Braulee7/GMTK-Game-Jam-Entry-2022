using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBehavior : MonoBehaviour
{
    public Goblin goblin {get; private set;}

    public GameObject nextPosition;
    public CapsuleCollider2D nextCapsule {get; private set;}
    public FollowBehaviour following {get; private set;}

    public LayerMask obstacleLayer;

    public void Awake()
    {
        this.goblin = GetComponent<Goblin>();
        nextCapsule = nextPosition.GetComponent<CapsuleCollider2D>();
        following = nextPosition.GetComponent<FollowBehaviour>();

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
        if (goblin.facingLeft) following.x = -Mathf.Abs(following.x);
        else following.x = Mathf.Abs(following.x);

        RaycastHit2D below = Physics2D.BoxCast(nextCapsule.bounds.center, nextCapsule.bounds.size, 0f, Vector2.down, 1.0f, obstacleLayer);
        RaycastHit2D forward;

        if (goblin.facingLeft)
        {
            forward = Physics2D.Raycast(nextCapsule.bounds.center, Vector2.left, 1.0f, obstacleLayer);
        } else 
        {
            forward = Physics2D.Raycast(nextCapsule.bounds.center, Vector2.right, 1.0f, obstacleLayer);
        }

        if (below.collider == null || forward.collider != null) return true;
        return false;
    }
}
