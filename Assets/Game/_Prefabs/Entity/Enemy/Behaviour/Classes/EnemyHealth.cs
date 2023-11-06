using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : Health
    {
        public HealthData data;
        public Enemy entity;
        public override void GetHurt(float damage)
        {
            data.HP -= damage;
            Debug.Log("Ouch! new hp is: " + data.HP);

            if(data.HP <= 0)
                Die();
        }
        public override void SetHealth(float value)
        {
            throw new System.NotImplementedException();
        }
        public override void Die()
        {
            entity.Kill();
        }
    }
}