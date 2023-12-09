using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMeleeChase : MeleeChase
{
    // data class name: chaseStats
    public override void ChaseTarget()
    {
        Vector2 vectorToAdd = new(chaseStats.chaseSpeed * Time.fixedDeltaTime * chaseStats.brain.direction,0);
        chaseStats.brain.SetDirection(Mathf.Sign(chaseStats.brain.distanceToTarget));
        chaseStats.brain.MoveEntity(vectorToAdd);
    }

    public override void EvaluateNextState()
    {
        float attackRange = chaseStats.brain.meleeAttack.attackStats.rangeSize;
        ChaseTarget();
        
        if(chaseStats.brain.target == null)
            chaseStats.brain.SetState(MeleeState.Idle);
        else if(Mathf.Abs(chaseStats.brain.distanceToTarget) < attackRange)
            chaseStats.brain.SetState(MeleeState.Attack);
    }
}
