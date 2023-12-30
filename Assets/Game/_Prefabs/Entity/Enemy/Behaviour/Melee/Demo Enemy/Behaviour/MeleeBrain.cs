using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBrain : MonoBehaviour , IHealth
{
    [Header("Config Values")]
    public Rigidbody2D body;
    public MeleeIdle meleeIdle;
    public MeleeChase meleeChase;
    public MeleeAttack meleeAttack;
    public MeleeState currentState;
    public LayerMask targetLayer;
    public Vector2 attentionRange;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float hp;

    //[HideInInspector]
    public float direction;
    //[HideInInspector]
    public float distanceToTarget;
    //[HideInInspector]
    public GameObject target;
    [HideInInspector]
    public bool movementLocked;

    public void SetState(MeleeState newState){
        currentState = newState;
    }

    public void MoveEntity(Vector2 diff){
        if(movementLocked)
            return;
        
        body.MovePosition(body.position + diff);
    }
    public void SetDirection(float value){
        direction = value;
    }
    private void CheckForEnemies(){
        RaycastHit2D hit = Physics2D.BoxCast(transform.position,attentionRange,0,new (direction,0),0,targetLayer);

        if(hit.collider == null){
            target = null;
            return;
        }

        target = hit.collider.gameObject;

        float currentX = transform.position.x;
        float targetX = target.transform.position.x;
        distanceToTarget = targetX - currentX;
    }
    private void FixedUpdate(){
        
        if (direction == 1)
            spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;

        CheckForEnemies();

        switch (currentState)
        {
            case MeleeState.Idle:
                meleeIdle.EvaluateNextState();
                break;
            case MeleeState.Chase:
                meleeChase.EvaluateNextState();
                break;
            case MeleeState.Attack:
                meleeAttack.EvaluateNextState();
                break;
        }
    }

    public void SetHealth(float value)
    {
        hp = value;
    }

    public void GetHurt(float damage)
    {
        print(hp);
        hp -= damage;
        if (hp <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}

public enum MeleeState{
    Idle,
    Chase,
    Attack
}
