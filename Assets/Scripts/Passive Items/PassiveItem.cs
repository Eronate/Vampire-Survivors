using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats player;
    public PassiveItemScriptableObject passiveItemData;
    public virtual void LevelUp()
    {
        passiveItemData.Level += 1;
    }
    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>();
    }
}
