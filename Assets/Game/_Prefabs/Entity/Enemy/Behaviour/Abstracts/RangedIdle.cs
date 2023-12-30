using UnityEngine;

public abstract class RangedIdle : MonoBehaviour{
    [System.Serializable]
    public class RangedIdleStats{
        public RangedBrain brain;
        public Animator animator;
    }
    public abstract void EvaluateNextState();
    public abstract void Idle();
    public RangedIdleStats idleStats;
}