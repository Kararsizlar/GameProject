using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementData : MonoBehaviour
{
    public float direction;
    public bool dashing;
    public LayerMask layerMask;
    private void Awake(){
        layerMask = LayerMask.GetMask("HardWall");
    }

    public bool CastCheck(Vector2 pos){
        RaycastHit2D hit = Physics2D.CircleCast(pos,0.03f,Vector2.zero,0,layerMask);
        return hit.collider != null;
    }
}

