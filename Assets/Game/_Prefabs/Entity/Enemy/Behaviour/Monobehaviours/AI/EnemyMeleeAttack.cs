using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField] float rangeCircleSize;
    [SerializeField] float waitBeforeAttack;
    [SerializeField] float waitAfterAttack;
    [SerializeField] GameObject enemyAttackPrefab;
    private bool isEnemyInRange;
    private bool onCooldown;

    private void TryAttack(){
        if(isEnemyInRange && !onCooldown)
            StartCoroutine(Attack());
    }

    private IEnumerator Attack(){
        onCooldown = true;
        yield return new WaitForSeconds(waitBeforeAttack);
        Instantiate(enemyAttackPrefab,transform.position,Quaternion.identity);

        yield return new WaitForSeconds(waitAfterAttack);
        onCooldown = false;
    }

    private bool CheckRange(){
        return true;
    }
}
