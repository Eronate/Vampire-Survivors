using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    protected float currentCooldown;
    protected int currentPierce;
    protected int currentLevel;

    protected PlayerMovement pm;
    private void Awake()
    {
        currentLevel = weaponData.Level;
        currentPierce = weaponData.Pierce;
        currentCooldown = weaponData.CooldownDuration;
    }
    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        // currentCooldown = weaponData.CooldownDuration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown<=0f) // once the cooldown is 0 attack
        {
            Attack();
        }
    }
    protected virtual void Attack()
    {
        currentCooldown = weaponData.CooldownDuration;
    }
    public virtual void LevelUp()
    {
        currentLevel += 1;
    }
}
