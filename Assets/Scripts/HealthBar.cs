using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider {get; private set;}

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    void Update()
    {
        slider.value = FindObjectOfType<Player>().currentHealth;
    }
}
