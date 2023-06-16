using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : WeaponController
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedBook = Instantiate(weaponData.Prefab);
        spawnedBook.transform.position = transform.position + new Vector3(3f, 3f, 0);
        spawnedBook.transform.parent = transform;
        BookBehaviour spawnedBookBehaviour = spawnedBook.GetComponent<BookBehaviour>();
        spawnedBookBehaviour.currentCooldownDuration = currentCooldown;
        spawnedBookBehaviour.currentDamage = weaponData.Damage;
        spawnedBookBehaviour.currentLevel = currentLevel;
        spawnedBookBehaviour.currentPierce = currentPierce;
        spawnedBookBehaviour.currentSpeed = weaponData.Speed;
    }
    public override void LevelUp()
    {
        base.LevelUp();
        currentCooldown = 4 + 3 / Mathf.Sqrt(currentLevel);
    }
}
