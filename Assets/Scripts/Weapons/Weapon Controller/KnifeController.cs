using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponController
{

    protected override void Start()
    {
        base.Start();
    }


    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(weaponData.Prefab);
        spawnedKnife.transform.position = transform.position;
        spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(pm.lastMovedVector); 
    }
    public override void LevelUp()
    {
        base.LevelUp();
        currentPierce = currentLevel;
        currentCooldown = (float)(0.5 + 1.5 / Mathf.Sqrt(currentLevel));
        // Debug.Log(weaponData.Pierce);
    }

}
