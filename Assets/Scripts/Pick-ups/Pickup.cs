using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ICollectible gobj = gameObject.TryGetComponent(out ICollectible obj) ? obj : null;
            gobj.Collect();
            Destroy(gameObject);
        }
    }
}
