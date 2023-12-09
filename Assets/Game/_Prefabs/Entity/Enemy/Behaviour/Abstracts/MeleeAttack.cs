using UnityEngine;

public abstract class MeleeAttack : MonoBehaviour{
    [System.Serializable]
    public class MeleeAttackStats{
        public MeleeBrain brain; 
        public Rigidbody2D body;
        public bool cooldown;
        public float rangeSize;
        public float waitBeforeAttack;
        public float waitAfterAttack;
        public GameObject enemyAttackPrefab;
    }
    public abstract void EvaluateNextState();
    public abstract void Attack();
    public MeleeAttackStats attackStats;
}