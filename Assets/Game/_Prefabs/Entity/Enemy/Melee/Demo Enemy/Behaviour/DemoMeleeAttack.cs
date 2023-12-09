using System;
using System.Collections;
using UnityEngine;

public class DemoMeleeAttack : MeleeAttack
{   
    //data class name: attackStats

    private WaitForSeconds timeBeforeAttack,timeAfterAttack;

    private IEnumerator IEAttack(){      
        yield return timeBeforeAttack;
        Instantiate(attackStats.enemyAttackPrefab,attackStats.body.position,Quaternion.identity);
        yield return timeAfterAttack;
        attackStats.cooldown = false;
        attackStats.brain.movementLocked = false;
    }

    public override void Attack()
    {
        if(attackStats.cooldown)
            return;
        
        attackStats.brain.movementLocked = true;
        StartCoroutine(IEAttack());
        attackStats.cooldown = true;
    }

    public override void EvaluateNextState()
    {
        attackStats.brain.SetDirection(Mathf.Sign(attackStats.brain.distanceToTarget));
        Attack();

        if(attackStats.brain.target == null)
            attackStats.brain.SetState(MeleeState.Idle);

        if(attackStats.rangeSize < Mathf.Abs(attackStats.brain.distanceToTarget))
            attackStats.brain.SetState(MeleeState.Chase);
    }

    private void Start(){
        timeBeforeAttack = new WaitForSeconds(attackStats.waitBeforeAttack);
        timeAfterAttack = new WaitForSeconds(attackStats.waitAfterAttack);
    }
}
