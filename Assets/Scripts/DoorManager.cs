using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public Door doorOne {get; private set;}
    public Door doorTwo {get; private set;}
    public Dice diceOne {get; private set;}
    public Dice diceTwo {get; private set;}
    public ButtonDoor button {get; private set;}

    [Header ("Select rule for the doors")]

    [Space (5)]
    [Header ("One door will be odd one will be even")]
    public bool OddsEvens;

    [Space (5)]
    [Header ("A specific number assigned to one door (Between 2 and 12)")]
    public bool SpecificNum;
    

    [Space (5)]
    [Header ("An inequality based door")]
    public bool GreaterLess;
    

    [Space (5)]
    [Header ("Number for the inequality and specific doors")]
    public int number;


    private void Awake()
    {
        doorOne = transform.Find("DoorOne").gameObject.GetComponent<Door>();
        doorTwo = transform.Find("DoorTwo").gameObject.GetComponent<Door>();

        diceOne = transform.Find("DiceOne").gameObject.GetComponent<Dice>();
        diceTwo = transform.Find("DiceTwo").gameObject.GetComponent<Dice>();

        button = transform.Find("Button").gameObject.GetComponent<ButtonDoor>();
    }

    private void Update()
    {
        //check if the button is pressed
        if (button.buttonPressed)
        {
            Invoke(nameof(ChooseDoor), 2.5f);
        }
    }

    private void ChooseDoor()
    {
        int decision = diceOne.num + diceTwo.num;

        if (OddsEvens && !SpecificNum && !GreaterLess)
        {
            if (decision % 2 == 0)
            {
                doorTwo.Open();
            } else 
            {
                doorOne.Open();
            }
        }

        if (!OddsEvens && SpecificNum && !GreaterLess)
        {
            if (decision == number)
            {
                doorOne.Open();
            } else
            {
                doorTwo.Open();
            }
        }

        if (!OddsEvens && !SpecificNum && GreaterLess)
        {
            if (decision < number)
            {
                doorOne.Open();
            } else
            {
                doorTwo.Open();
            }
        }       
    }


}
