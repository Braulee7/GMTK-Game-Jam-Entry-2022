using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviour : MonoBehaviour
{
    public Transform target;
    public float x, y, z;

    private void Update()
    {
        this.transform.position = target.transform.position + new Vector3 (x, y, z);
    }
}
