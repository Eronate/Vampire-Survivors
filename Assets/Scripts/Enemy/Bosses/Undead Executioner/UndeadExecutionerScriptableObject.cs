using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UndeadExecutionerScriptableObject", menuName = "ScriptableObjects/UndeadExecutioner")]
public class UndeadExecutionerScriptableObject : EnemyScriptableObject
{
    [SerializeField]
    int spellFrequency;
    public int SpellFrequency { get { return spellFrequency; } set { spellFrequency = value; } }
    [SerializeField]
    int healFrequency;
    public int HealFrequency { get { return healFrequency; } set { healFrequency= value; } }
    [SerializeField]
    float minTimeSinceLastAction;
    public float MinTimeSinceLastAction { get { return minTimeSinceLastAction; } set { minTimeSinceLastAction = value; } }
    [SerializeField]
    float maxTimeSinceLastAction;
    public float MaxTimeSinceLastAction { get { return maxTimeSinceLastAction; } set { maxTimeSinceLastAction = value; } }
}
