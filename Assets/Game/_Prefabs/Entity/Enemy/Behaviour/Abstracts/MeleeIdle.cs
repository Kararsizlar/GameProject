using UnityEngine;

public abstract class MeleeIdle : MonoBehaviour{
    [System.Serializable]
    public class MeleeIdleStats{
        public MeleeBrain brain; 
        public Rigidbody2D body;
        public bool waiting;
        public float idleSpeed;
        public float maxDistanceToNextSpot;
        public float multiplierPercentage;
        public float averageRestTime;
    }
    public abstract void EvaluateNextState();
    public abstract void Idle();
    public MeleeIdleStats idleStats;
}