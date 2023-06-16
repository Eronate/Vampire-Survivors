using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinachPassiveItem : PassiveItem
{
    public override void LevelUp()
    {
        base.LevelUp();
        player.CurrentMight = 2 - 1 / Mathf.Pow(level, 4 / 5);
    }
}
