using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public float lastHorizontalVector = 0;

    [HideInInspector]
    public float lastVerticalVector = 0;

    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 lastMovedVector;

    // public CharacterScriptableObject characterData; // no longer needed
    PlayerStats player;

    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        player=GetComponent<PlayerStats>(); 
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f); //projectile goes right if no movement
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    void FixedUpdate()
    {
        Move();
    }
    void InputManagement()
    {
        if(GameManager.instance.isOver)
        {
            return;
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;

        if(moveDir.x != 0)
        {

            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);
        }

        if(moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);
        }
        if(moveDir.x != 0 && moveDir.y!=0 )
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
    }
    void Move()
    {
        if (GameManager.instance.isOver)
        {
            return;
        }
        rb.velocity = new Vector2(moveDir.x * player.CurrentMoveSpeed, moveDir.y * player.CurrentMoveSpeed);
    }
}
