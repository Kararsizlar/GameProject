using UnityEngine;

public abstract class RangedAttack : MonoBehaviour{
    [System.Serializable]
    public class RangedAttackStats{
        public RangedBrain brain; 
        public Rigidbody2D body;
        public bool cooldown;
        public float rangeSize;
        public float waitBeforeAttack;
        public float waitAfterAttack;
        public float bulletSpeed;
        public GameObject enemyAttackPrefab;
        public Animator animator;
    }
    public abstract void EvaluateNextState();
    public abstract void Attack();
    public RangedAttackStats attackStats;
}