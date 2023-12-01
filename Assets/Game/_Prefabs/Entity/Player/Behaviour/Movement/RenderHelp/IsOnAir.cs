using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    public class IsOnAir : MonoBehaviour
    {
        [SerializeField] PlayerData playerData;
        private float scaleX,scaleY;
        void Start()
        {
            scaleX = playerData.boxCollider2D.bounds.size.x / 2;
            scaleY = playerData.boxCollider2D.bounds.size.y / 2;
        }

        public void Update(){
            
            Vector2 leftPos  = new(playerData.playerBody2D.position.x - scaleX,playerData.playerBody2D.position.y - scaleY);
            Vector2 rightPos = new(playerData.playerBody2D.position.x + scaleX,playerData.playerBody2D.position.y - scaleY);
            bool leftCheck = playerData.CircleCheck(leftPos,0.1f,Vector2.zero,playerData.softWall);
            bool rightCheck = playerData.CircleCheck(rightPos,0.1f,Vector2.zero,playerData.wall);

            if (leftCheck || rightCheck){
                playerData.onAir = false;
                EventHub.onGroundedEvent?.Invoke();
            }

            else
                playerData.onAir = true;          
        }
    }
}
