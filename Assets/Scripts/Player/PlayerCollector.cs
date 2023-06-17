using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollector;
    public float pullSpeed;
    [SerializeField]
    private AudioSource gemCollectSound;
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
        if (col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            StartCoroutine(_KeepFollowing(col));
        }
    }

    IEnumerator _KeepFollowing(Collider2D col)
    {
       while (col != null && col.gameObject.TryGetComponent(out ICollectible collectible) )
            {
                Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
                Vector2 forceDirection = (transform.position - col.transform.position).normalized;
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(forceDirection * pullSpeed);
                yield return new WaitForSeconds(0.02f);
            }
       gemCollectSound.Play();
    }
}
