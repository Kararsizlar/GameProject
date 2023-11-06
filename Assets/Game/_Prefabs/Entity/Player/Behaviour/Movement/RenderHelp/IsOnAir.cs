using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    public class IsOnAir : MonoBehaviour
    {
        [HideInInspector] public PlayerData playerData;
        private float scaleX,scaleY;
        void Start()
        {
            scaleX = playerData.boxCollider2D.bounds.size.x / 2;
            scaleY = playerData.boxCollider2D.bounds.size.y / 2;
        }

        private string GetCurrentClipName(){
            int layerIndex = 0;
            AnimatorClipInfo[] clipInfo = playerData.animator.GetCurrentAnimatorClipInfo(layerIndex); 
            return clipInfo[0].clip.name;
        }

        public void Update(){
                
            Vector2 leftPos  = new(playerData.playerBody2D.position.x - scaleX,playerData.playerBody2D.position.y - scaleY);
            Vector2 rightPos = new(playerData.playerBody2D.position.x + scaleX,playerData.playerBody2D.position.y - scaleY);
            bool leftCheck = playerData.CircleCheck(leftPos,0.1f,Vector2.zero,playerData.wall);
            bool rightCheck = playerData.CircleCheck(rightPos,0.1f,Vector2.zero,playerData.wall);

            if (leftCheck || rightCheck)
                playerData.onAir = false;
            else
                playerData.onAir = true;
            

            if(playerData.onAir && GetCurrentClipName() != "Player_Fall")
                playerData.animator.Play("Player_Fall");
            
            if(!playerData.onAir && playerData.onRun && GetCurrentClipName() != "Player_Run")
                playerData.animator.Play("Player_Run");

            if(!playerData.onAir && !playerData.onRun && GetCurrentClipName() != "Player_Idle")
                playerData.animator.Play("Player_Idle");
        }
    }
}
