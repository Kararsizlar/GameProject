using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoRangedIdle : RangedIdle
{
    public override void EvaluateNextState()
    {
        if (idleStats.brain.target != null)
            idleStats.brain.SetState(RangedState.Attack);

        Idle();
    }

    public override void Idle()
    {
        idleStats.animator.Play("RangedIdle");
    }
}
