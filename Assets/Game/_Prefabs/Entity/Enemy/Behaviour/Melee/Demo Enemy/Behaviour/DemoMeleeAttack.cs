using System;
using System.Collections;
using UnityEngine;

public class DemoMeleeAttack : MeleeAttack
{   
    //data class name: attackStats

    private WaitForSeconds timeBeforeAttack,timeAfterAttack;

    private IEnumerator IEAttack(){
        attackStats.animator.Play("MeleeAttack");
        yield return timeBeforeAttack;
        Instantiate(attackStats.enemyAttackPrefab,attackStats.body.position + new Vector2(attackStats.rangeSize / 2 * attackStats.brain.direction,0),Quaternion.identity);
        attackStats.brain.movementLocked = false;
        attackStats.animator.Play("MeleeIdle");
        yield return timeAfterAttack;
        attackStats.cooldown = false;
    }

    public override void Attack()
    {
        if(attackStats.cooldown)
            return;

        attackStats.brain.movementLocked = true;
        StartCoroutine(IEAttack());
        attackStats.cooldown = true;
        EvaluateNextState();
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
