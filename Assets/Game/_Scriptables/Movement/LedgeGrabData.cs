using UnityEngine;

[CreateAssetMenu(menuName = "Data/Movement/LedgeGrab")]
public class LedgeGrabData : ScriptableObject
{
    [Tooltip("Asılma süresi")]
    public float hangTime;
}
