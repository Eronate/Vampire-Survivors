using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralAnimator : MonoBehaviour
{
    public Animator animator;
    RatMovement ratMovement;
    SpriteRenderer spriteRenderer;
    float ratLife;

    void Start()
    {
        animator = GetComponent<Animator>();
        ratMovement = GetComponent<RatMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ratLife = Random.Range(15f, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if(ratMovement.moveDirection.x != 0 || ratMovement.moveDirection.y != 0)
        {
            animator.SetBool("Move", true);
            SpriteDirectionChecker();
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }
    void SpriteDirectionChecker()
    {
        if (ratMovement.moveDirection.x < 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }
    public void Die()
    {
        animator.SetBool("Die", true);
    }
    public void SetBirth()
    {
        animator.SetBool("Birth", true);
    }
}
