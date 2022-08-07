using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    public Slider slider {get; private set;}

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = FindObjectOfType<Player>().dashTimer;
    }
}
