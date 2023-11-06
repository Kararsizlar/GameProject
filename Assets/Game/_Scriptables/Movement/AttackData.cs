using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Movement/Attack")]
public class AttackData : ScriptableObject
{
    public List<Slash> attacks;
}