using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public SpriteRenderer sr {get; private set;}
    public Animator animator {get; private set;}
    public Sprite[] images;

    public int num;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        animator.enabled = false;

        //initialise to negative so managers know its not rolled
        num = -1;
    }

    public void Roll()
    {
        animator.enabled = true;
        animator.SetBool("Roll", true);

        num = Random.Range(1, 6);

        Invoke(nameof(StopRoll), 2.5f);
    }

    private void StopRoll()
    {
        animator.enabled = false;
        animator.SetBool("Roll", false);

        sr.sprite = images[num - 1];
    }

}
