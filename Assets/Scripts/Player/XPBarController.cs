using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarController : MonoBehaviour
{
    public Slider slider;
 

    public void SetXp(int xp)
    {
        slider.value = xp;
     
    }
    public void SetMaxXp(int xp)
    {
        slider.maxValue = xp;
        slider.value = xp;
      
    }
}
