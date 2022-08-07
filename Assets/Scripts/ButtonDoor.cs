using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{
    public SpriteRenderer sr {get; private set;}
    public Dice[] die;
    public Sprite pressed;
    
    public bool buttonPressed;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        
    }

    private void Start()
    {
        buttonPressed = false;
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.name == "Hero" && !buttonPressed)
        {
            //make the button pressed
            buttonPressed = true;
            sr.sprite = pressed;

            foreach (Dice dice in die)
            {
                dice.Roll();
            }
        }
    }


}
