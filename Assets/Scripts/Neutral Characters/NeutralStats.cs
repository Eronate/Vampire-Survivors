using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralStats : MonoBehaviour
{
    public NeutralScriptableObject neutralData;
    float currentHealth;
    public float despawnDistance = 20f;
    private void Awake()
    {
        currentHealth = neutralData.MaxHealth;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Kill()
    {
        Destroy(gameObject);
    }
}
