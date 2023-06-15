using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicContorller : WeaponController
{ 
    protected override void Start()
    {
        base.Start();
    }

  
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGarlic = Instantiate(weaponData.Prefab);
        spawnedGarlic.transform.position= transform.position;
        spawnedGarlic.transform.parent = transform;
    }
    public override void LevelUp()
    {
        base.LevelUp();
        weaponData.CooldownDuration = 2 + 1 / weaponData.Level;
    }
}
