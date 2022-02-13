using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public bool doonceish = true;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        if(doonceish){slider.value = health; doonceish = false;}
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
