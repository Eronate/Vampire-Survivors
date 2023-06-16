using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats player;
    public PassiveItemScriptableObject passiveItemData;
    protected int level;
    private void Awake()
    {
        level = passiveItemData.Level;
    }
    public virtual void LevelUp()
    {
        level += 1;
    }
    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>();
    }
}
