using UnityEngine;

[System.Serializable]
public class JumpData {
    public float speed = 18;
    public float maxDistance = 4.1f;

    //Runtime
    [HideInInspector] public bool jumpInputting; 
}