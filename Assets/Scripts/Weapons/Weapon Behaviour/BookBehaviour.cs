using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehaviour : ProjectileWeaponBehaviour
{
    Transform stats;
    //int currentSpawnedBooks;
    //int numberOfBooks;
    //float rangeFromPlayer = 3f;
    protected override void Start()
    {
        base.Start();
        stats = FindObjectOfType<PlayerStats>().transform;
        //numberOfBooks = weaponData.Level; //number of books = Book's level.
    }

    public override void DirectionChecker(Vector3 dir)
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {
        var lookDir = Vector3.Cross(transform.position - stats.position, Vector3.up);
        float radius = 3f; 
        float angle = Time.time * weaponData.Speed; // Angle based on speed

        float x = stats.position.x + Mathf.Cos(angle) * radius;
        float y = stats.position.y + Mathf.Sin(angle) * radius;

        transform.position = new Vector3(x, y, 0);
        transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);

    }

}
