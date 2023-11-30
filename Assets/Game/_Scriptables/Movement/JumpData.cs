using UnityEngine;

[System.Serializable]
public class JumpData {
    public float speed = 18;
    public float maxDistance = 4.1f;
    public float downJumpDistance;

    //Runtime
    [HideInInspector] public bool jumpInputting; 
}