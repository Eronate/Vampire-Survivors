using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponScriptableObject",menuName ="ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{

    [SerializeField]

    GameObject prefab;
    public GameObject Prefab { get => prefab; private set=> prefab = value;  }

    [SerializeField]

    float damage;
    public float Damage { get => damage; private set => damage = value; }
    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }
    [SerializeField]
    float  cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; set => cooldownDuration = value; }
    [SerializeField]
    int pierce;
    public int Pierce { get => pierce; set => pierce = value; }

    [SerializeField]
    int level;
    public int Level { get => level; set => level = value; }

    [SerializeField]
    Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }
}
