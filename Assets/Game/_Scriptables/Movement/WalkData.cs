using UnityEngine;

[System.Serializable]
public class WalkData
{
    public float speed = 8f;
    public float minSpeedBeforeStop = 0.25f;
    [Range(0,1)]public float slowDownMultiplier = 0.85f;


    //Runtime Stuff
    [HideInInspector] public bool interrupted = false;
    [HideInInspector] public int walkInput;
}
