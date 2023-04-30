using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }
    [SerializeField]
    float damage;
    public float Damage {get => damage; private set=> damage = value;}
    [SerializeField]
    float speed;
    public float Speed {get=> speed; private set => speed = value; }
    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration {get => cooldownDuration; private set => cooldownDuration = value;}
    [SerializeField]
    int pierce;
    public int Pierce {get=> pierce; private set => pierce = value; }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
