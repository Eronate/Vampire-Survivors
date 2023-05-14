using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EvilWizardScriptableObject", menuName = "ScriptableObjects/EvilWizard")]
public class EvilWizardScriptableObject : EnemyScriptableObject
{
    [SerializeField]
    int baseAttackFrequency;
    public int BaseAttackFrequency { get { return baseAttackFrequency; } set { baseAttackFrequency = value; } }
    [SerializeField]
    float specialAttackMultipler;
    public float SpecialAttackMultipler { get { return specialAttackMultipler; } set { specialAttackMultipler = value; } }
}
