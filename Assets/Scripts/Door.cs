using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator {get; private set;}
    public BoxCollider2D box {get; private set;}

    private void Awake()
    {
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    public void Open()
    {
        animator.SetTrigger("Open");
        box.enabled = false;
    }
}
