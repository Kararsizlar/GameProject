using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    public class IsOnAir : MonoBehaviour
    {
        [SerializeField] PlayerData playerData;
        private float spacing = 0.1f;

        public void Update(){
            Vector2 min = playerData.boxCollider2D.bounds.min;
            Vector2 max = playerData.boxCollider2D.bounds.max;
            Vector2 leftPos  = new(min.x - spacing,min.y);
            Vector2 rightPos = new(max.x - spacing,min.y);
            bool leftCheck = playerData.CircleCheck(leftPos,0.1f,Vector2.zero,playerData.softWall);
            bool rightCheck = playerData.CircleCheck(rightPos,0.1f,Vector2.zero,playerData.wall);

            if(playerData.onJump || playerData.onAir == false)
                return;

            if (leftCheck || rightCheck){
                playerData.onAir = false;
                EventHub.onGroundedEvent?.Invoke();
            }

            else
                playerData.onAir = true;          
        }
    }
}
