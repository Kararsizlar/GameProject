using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoRangedAttack : RangedAttack
{
    bool cooldown = false;
    public void StopCooldown()
    {
        cooldown = false;
    }
    public override void Attack()
    {
        if (cooldown)
            return;

        cooldown = true;
        Invoke(nameof(StopCooldown), attackStats.waitAfterAttack);
        attackStats.animator.Play("RangedAttack");
        GameObject bullet = Instantiate(attackStats.enemyAttackPrefab,transform.position,Quaternion.identity);
        Vector2 direction = attackStats.brain.target.transform.position - bullet.transform.position;
        Vector2 normalized = direction.normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = normalized * attackStats.bulletSpeed;
    }

    public override void EvaluateNextState()
    {
        attackStats.brain.SetDirection(Mathf.Sign(attackStats.brain.distanceToTarget));
        Attack();

        if (attackStats.brain.target == null)
            attackStats.brain.SetState(RangedState.Idle);
    }
}
