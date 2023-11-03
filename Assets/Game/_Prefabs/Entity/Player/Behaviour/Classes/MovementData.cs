using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementData : MonoBehaviour
{
    public float direction;
    public bool canDash;
    public bool dashing;
    public LayerMask wall;
    public LayerMask hittable;
    public bool onAttack;
    public int currentCombo;

    private void Awake(){
        direction = 1;
        wall = LayerMask.GetMask("HardWall");
        hittable = LayerMask.GetMask("Enemy");
        canDash = true;
    }

    public bool CircleCheck(Vector2 pos){
        RaycastHit2D hit = Physics2D.CircleCast(pos,0.03f,Vector2.zero,0,wall);
        return hit.collider != null;
    }
}

