using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsPassiveItem : PassiveItem
{
    public override void LevelUp()
    {
        base.LevelUp();
        player.CurrentMoveSpeed = 6 - 1 / level;
    }
}
