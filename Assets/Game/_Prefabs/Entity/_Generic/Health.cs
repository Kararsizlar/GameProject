using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health
{
    public abstract void SetHealth(float value);
    public abstract void GetHurt(float damage);
    public abstract void Die();
}
