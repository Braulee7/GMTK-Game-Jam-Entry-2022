using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public float health;

    private void OnCollisionEnter2D (Collision2D collider)
    {
        if (collider.gameObject.name == "Hero")
        {
            collider.gameObject.GetComponent<Player>().Heal(health);
            this.gameObject.SetActive(false);
        }
    }
}
