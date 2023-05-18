using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollector;
    public float pullSpeed;
     void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        playerCollector= GetComponent<CircleCollider2D>();

    }
    void Update() 
    {
        playerCollector.radius = player.CurrentMagnet;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            //Pulling animation
            //Gets the Rigidbody2D component on the item
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDirection = (transform.position - col.transform.position).normalized;

            rb.AddForce(forceDirection * pullSpeed);
            collectible.Collect();
        }
    }
}
