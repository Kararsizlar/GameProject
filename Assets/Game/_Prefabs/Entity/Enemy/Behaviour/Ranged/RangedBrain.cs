using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBrain : MonoBehaviour , IHealth
{
    [Header("Config Values")]
    public RangedIdle rangedIdle;
    public RangedAttack rangedAttack;
    public RangedState currentState;
    public LayerMask targetLayer;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float distanceToTarget;
    public float hp;

    public float direction;
    public GameObject target;
    [HideInInspector]
    public bool movementLocked;

    public void SetState(RangedState newState)
    {
        currentState = newState;
    }
    public void SetDirection(float value)
    {
        direction = value;
    }
    private void CheckForEnemies()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, distanceToTarget,Vector2.zero,0, targetLayer);

        if (hit.collider == null)
        {
            target = null;
            return;
        }

        target = hit.collider.gameObject;
    }
    private void FixedUpdate()
    {

        if (direction == 1)
            spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;

        CheckForEnemies();

        switch (currentState)
        {
            case RangedState.Idle:
                rangedIdle.EvaluateNextState();
                break;
            case RangedState.Attack:
                rangedAttack.EvaluateNextState();
                break;
        }
    }
    public void SetHealth(float value)
    {
        hp = value;
    }

    public void GetHurt(float damage)
    {
        hp -= damage;
        if (hp <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}


public enum RangedState
{
    Idle,
    Attack
}
