using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    protected Vector3 direction;
    public float destroyAfterSeconds;

    // Current stats
    [HideInInspector]
    public float currentDamage;
    [HideInInspector]
    public float currentSpeed;
    [HideInInspector]
    public float currentCooldownDuration;
    [HideInInspector]
    public int currentPierce;
    [HideInInspector]
    public int currentLevel;

    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    // Update is called once per frame
    public virtual void DirectionChecker(Vector3 dir)
    {
        direction = dir;
        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale; // tranform the orientation of the object based on the parent object which is the player.
        Vector3 rotation = transform.rotation.eulerAngles;
        if(dirx<0 && diry==0) //left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        else if (dirx==0 && diry<0)  //down
        {
            scale.y = scale.y * -1;
        }
        else if (dirx == 0 && diry > 0)  //up
        {
            scale.x = scale.x * -1;
        }
        else if (dirx > 0 && diry > 0)  //right up
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = 0f;
           
        }
        else if (dirx > 0 && diry < 0)  //right down
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f;
        
        }
        else if (dirx > 0 && diry < 0)  //right down
        {
            rotation.z = -90f;
        }
        else if (dirx < 0 && diry > 0)  //right down
        {
            rotation.z = -90f;
        }
        else if (dirx < 0 && diry < 0)  //right down
        {
            scale.y = scale.y * -1;
            rotation.z = 90f;

        }


        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);

    }
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());
            ReducePierce();
        }
        else if(col.CompareTag("Prop"))
        {
            if(col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
                ReducePierce();
            }
        }
    }
    void ReducePierce()
    {
        currentPierce--;
        if(currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
