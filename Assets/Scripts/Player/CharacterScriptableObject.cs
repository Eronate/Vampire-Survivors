using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject startingWeapon;
    public GameObject StartingWeapon { get => startingWeapon; private set => startingWeapon = value; }

    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => MaxHealth; private set => MaxHealth = value; }

    [SerializeField]
    float recovery;
    public float Recovery { get => Recovery; private set => Recovery = value; }

    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => MoveSpeed; private set => MoveSpeed = value; }

    [SerializeField]
    float might;
    public float Might { get => Might; private set => Might = value; }

    [SerializeField]
    float projectileSpeed;
    public float ProjectileSpeed { get => ProjectileSpeed; private set => ProjectileSpeed = value; }
}
