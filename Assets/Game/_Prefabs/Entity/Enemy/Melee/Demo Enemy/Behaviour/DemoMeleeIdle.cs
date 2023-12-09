using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMeleeIdle : MeleeIdle
{
    //data class name: idleStats
    [SerializeField] float targetX;
    public override void EvaluateNextState()
    {
        if(idleStats.brain.target != null)
            idleStats.brain.SetState(MeleeState.Chase);
            
        Idle();
    }

    public override void Idle()
    {
        if(idleStats.waiting)
            return;

        float currentX = idleStats.body.position.x;
        float diff = targetX - currentX;
        idleStats.brain.SetDirection(Mathf.Sign(diff));

        if(Mathf.Abs(targetX - currentX) < 0.25f){
            SetNewTarget();
            StartCoroutine(WaitTimer());
            idleStats.waiting = true;
        }

        idleStats.brain.MoveEntity(new(idleStats.brain.direction * Time.fixedDeltaTime * idleStats.idleSpeed,0));
    }
    private IEnumerator WaitTimer(){
        float minWait = 1 - idleStats.multiplierPercentage;
        float maxWait = 1 + idleStats.multiplierPercentage;
        float timeToWait = Random.Range(minWait * idleStats.averageRestTime,maxWait * idleStats.averageRestTime);
        yield return new WaitForSeconds(timeToWait);
        idleStats.waiting = false;
    }

    private void SetNewTarget(){
        float diff = Random.Range(-idleStats.maxDistanceToNextSpot,idleStats.maxDistanceToNextSpot);
        print(diff);
        targetX = idleStats.body.position.x + diff;
    }

    private void Start(){
        SetNewTarget();
    }
}
