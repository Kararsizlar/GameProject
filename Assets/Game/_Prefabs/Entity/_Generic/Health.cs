using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public void SetHealth(float value);
    public void GetHurt(float damage);
    public void Die();
}
