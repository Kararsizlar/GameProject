using UnityEngine;

public abstract class MeleeChase : MonoBehaviour{
    [System.Serializable]
    public class MeleeChaseStats{
        public MeleeBrain brain; 
        public Rigidbody2D body;
        public float chaseSpeed;
    }
    public abstract void EvaluateNextState();
    public abstract void ChaseTarget();
    public MeleeChaseStats chaseStats;
}