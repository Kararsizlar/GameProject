using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    [System.Serializable]
    public class PlayerData : MonoBehaviour
    {
        public float direction;
        public bool onAttack;
        public bool onRun;
        public bool onAir;  
        public Transform playerTransform;
        public Rigidbody2D playerBody2D;
        public BoxCollider2D boxCollider2D;
        public SpriteRenderer playerRenderer;
        public Animator animator;

        public bool canDash;
        public bool dashing;
        public LayerMask wall;
        public LayerMask hittable;
        public int currentCombo;

        public bool CircleCheck(Vector2 position,float radius,Vector2 direction, LayerMask mask){
            RaycastHit2D hit = Physics2D.CircleCast(position,radius,direction,0,mask);
            return hit.collider != null;
        }

        public bool BoxCheck(Vector2 position,Vector2 size,Vector2 direction, LayerMask mask){
            RaycastHit2D hit = Physics2D.BoxCast(position,size,0,direction,0,mask);
            return hit.collider != null;
        }

        public RaycastHit2D[] BoxCheckAll(Vector2 position,Vector2 size,Vector2 direction, LayerMask mask){
            RaycastHit2D[] hit = Physics2D.BoxCastAll(position,size,0,direction,0,mask);
            return hit;
        }
    }
}

