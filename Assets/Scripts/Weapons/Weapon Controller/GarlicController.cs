using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

  
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGarlic = Instantiate(weaponData.Prefab);
        spawnedGarlic.transform.localScale = (spawnedGarlic.transform.localScale + new Vector3(Mathf.Atan(currentLevel), Mathf.Atan(currentLevel), 0));
        Vector3 scaleMultiplier = new Vector3(0.27f, 0.27f, 0.27f);
        spawnedGarlic.transform.localScale = Vector3.Scale(spawnedGarlic.transform.localScale, scaleMultiplier);
        spawnedGarlic.transform.position= transform.position;
        spawnedGarlic.transform.parent = transform;
        GarlicBehaviour spawnedGarlicBehaviour = spawnedGarlic.GetComponent<GarlicBehaviour>();
        spawnedGarlicBehaviour.currentDamage = weaponData.Damage;
        spawnedGarlicBehaviour.currentSpeed = weaponData.Speed;
        spawnedGarlicBehaviour.currentPierce = currentPierce;
        spawnedGarlicBehaviour.currentCooldownDuration = currentCooldown;
    }
    public override void LevelUp()
    {
        base.LevelUp();
        currentCooldown = 2 + 1 / currentLevel;
    }
}
