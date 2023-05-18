using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RatMovement : MonoBehaviour
{
    public float minMoveDuration = 5f;
    public float maxMoveDuration = 9f;

    NeutralStats rat;
    public Vector2 moveDirection = Vector2.zero;
    float actionDuration = 0f;
    float ratLife;
    int actionType;    // 0 if it's idle or 1 for moving;

    void Start()
    {
        ratLife = Random.Range(15f, 20f);
        rat = GetComponent<NeutralStats>();
        ChooseNewAction();
    }

    // Update is called once per frame
    void Update()
    {
        actionDuration -= Time.deltaTime;
        ratLife -= Time.deltaTime;
        if (ratLife <= 0f)
        {
            GetComponent<NeutralAnimator>().Die();
        }
        if (actionType == 1)
        {
            transform.Translate(moveDirection * rat.neutralData.MoveSpeed * Time.deltaTime);
        }
        if(actionDuration <= 0f)
        {
            ChooseNewAction();
        }
    }
    void ChooseNewAction()
    {
        actionDuration = Random.Range(minMoveDuration, maxMoveDuration);
        actionType = Random.Range(0, 3);
        if(actionType == 1)
        {
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            transform.Translate(moveDirection * rat.neutralData.MoveSpeed * Time.deltaTime);
        }
        else
        {
            moveDirection = Vector2.zero;
        }
    }
}
