using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWeaponImageController : MonoBehaviour
{
    public List<Sprite> weapons;
    void Start()
    {
        if(GameManager.instance.index < weapons.Count)
        {
            GetComponent<Image>().sprite = weapons[GameManager.instance.index];
        }
        else
        {
            Debug.Log("There is no weapon with this index in the MainWeaponImageController's weapons array");
        }
    }
}
