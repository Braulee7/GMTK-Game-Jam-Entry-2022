using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int points;

    private void OnCollisionEnter2D (Collision2D collider)
    {
        if (collider.gameObject.name == "Hero")
        {
            FindObjectOfType<GameManager>().SetScore(points);

            this.gameObject.SetActive(false);
        }
    }
}
