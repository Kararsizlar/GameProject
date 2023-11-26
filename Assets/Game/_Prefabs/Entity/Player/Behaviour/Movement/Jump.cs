using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace PlayerSpace{
    public class Jump : MonoBehaviour , IPlayerMovementAction
    {
        private Coroutine jumpCoroutine;
        [HideInInspector] public PlayerData playerData;
        public JumpData jumpData;
        private float scaleX;
        private float scaleY;
        private bool cancelFlag;

        private void Start(){
            scaleX = playerData.boxCollider2D.bounds.size.x / 2 - 0.1f;
            scaleY = playerData.boxCollider2D.bounds.size.y / 2;
        }

        private void StopJump(){
            if(jumpCoroutine == null)
                return;
            
            StopCoroutine(jumpCoroutine);
            jumpCoroutine = null;
            playerData.onJump = false;
            playerData.playerBody2D.velocity= new Vector2(playerData.playerBody2D.velocity.x,0);
        }

        private IEnumerator IStartJump(){
            float startY = playerData.playerBody2D.position.y;
            Vector2 leftPos  = new(playerData.playerBody2D.position.x - scaleX,playerData.playerBody2D.position.y + scaleY);
            Vector2 rightPos = new(playerData.playerBody2D.position.x + scaleX,playerData.playerBody2D.position.y + scaleY);
            playerData.animator.Play("Player_Jump");
            playerData.onJump = true;
            while (playerData.playerBody2D.position.y - startY < jumpData.maxDistance && !playerData.CircleCheck(rightPos,0.1f,Vector2.zero,playerData.wall) && !playerData.CircleCheck(leftPos,0.1f,Vector2.zero,playerData.wall)){
                
                if(cancelFlag){
                    cancelFlag = false;
                    break;
                }

                leftPos  = new(playerData.playerBody2D.position.x - scaleX,playerData.playerBody2D.position.y + scaleY);
                rightPos = new(playerData.playerBody2D.position.x + scaleX,playerData.playerBody2D.position.y + scaleY);
                yield return Time.fixedDeltaTime;
                playerData.playerBody2D.velocity = new(playerData.playerBody2D.velocity.x,jumpData.speed);
            }
            StopJump();
        }

        public void DoAction(InputAction.CallbackContext callback){

            void StartJump(){
                Vector2 leftPos  = new(playerData.playerBody2D.position.x - scaleX,playerData.playerBody2D.position.y - scaleY);
                Vector2 rightPos = new(playerData.playerBody2D.position.x + scaleX,playerData.playerBody2D.position.y - scaleY);
                
                if (playerData.CircleCheck(leftPos,0.1f,Vector2.zero,playerData.wall) || playerData.CircleCheck(rightPos,0.1f,Vector2.zero,playerData.wall))
                    jumpCoroutine = StartCoroutine(IStartJump());
            }


            if(callback.action.name == "Jump")
                StartJump();
            else if(callback.action.name == "Jump Cancel" && jumpCoroutine != null)
                cancelFlag = true;
        }

        private void OnDrawGizmos(){
            if(!Application.isPlaying)
                return;

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(new(playerData.playerBody2D.position.x - scaleX,playerData.playerBody2D.position.y - scaleY), 0.1f);
            Gizmos.DrawSphere(new(playerData.playerBody2D.position.x + scaleX,playerData.playerBody2D.position.y - scaleY), 0.1f);
        }
    }
}
