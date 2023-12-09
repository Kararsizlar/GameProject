using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthData : IHealth
{
    public GameObject entity;
    public float HP;
    public float resistance;

    public void Die()
    {
        GameObject.Destroy(entity);
    }

    public void GetHurt(float damage)
    {
        HP -= damage;
        if(HP <= 0)
            Die();
    }

    public void SetHealth(float value)
    {
        HP = value;
    }
}
