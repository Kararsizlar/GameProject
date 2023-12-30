using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace PlayerSpace{
    public class Jump : MonoBehaviour
    {
        [SerializeField] PlayerData playerData;
        [SerializeField] JumpData jumpData;
        private JumpHelper jumpHelper;

        private void Start(){
            jumpHelper = new(playerData,this,jumpData);
        }
        public void DoAction(InputAction.CallbackContext callback){
            
            if(callback.phase == InputActionPhase.Started){
                jumpData.jumpInputting = true;
                jumpHelper.StartJump();
            }

            if(callback.phase == InputActionPhase.Canceled){
                jumpData.jumpInputting = false;
                jumpHelper.StopJump();
            }
        }
    }



    class JumpHelper{
        private float scaleX,scaleY;
        private PlayerData playerData;
        private Coroutine jumpCoroutine;
        private Jump jump;
        private JumpData jumpData;

        private IEnumerator IEJump(){
            float startY = playerData.playerBody2D.position.y;
            playerData.onJump = true;
            playerData.onAir = true;
            while (playerData.playerBody2D.position.y - startY < jumpData.maxDistance && playerData.onAir){
                yield return Time.fixedDeltaTime;
                playerData.playerBody2D.velocity = new(playerData.playerBody2D.velocity.x,jumpData.speed);
            }
            StopJump();
        }

        private void IEDownJump(){
            playerData.playerBody2D.transform.position += Vector3.down * jumpData.downJumpDistance;
        }

        public void StopJump(){
            if(jumpCoroutine == null)
                return;
            
            EventHub.jumpStoppedEvent?.Invoke();
            jump.StopCoroutine(jumpCoroutine);
            jumpCoroutine = null;
            playerData.onJump = false;
            playerData.playerBody2D.velocity = new Vector2(playerData.playerBody2D.velocity.x,0);
        }

        public void StartJump(){
            EventHub.jumpStartedEvent?.Invoke();
            Vector2 p1 = new(playerData.boxCollider2D.bounds.min.x,playerData.boxCollider2D.bounds.min.y);
            Vector2 p2 = new(playerData.boxCollider2D.bounds.max.x,playerData.boxCollider2D.bounds.min.y);
            bool isPlayerInSoftWall = playerData.CircleCheck(p1,0.1f,Vector2.zero,playerData.softWall) || playerData.CircleCheck(p2,0.1f,Vector2.zero,playerData.softWall);
            bool isPlayerInHardWall = playerData.CircleCheck(p1,0.1f,Vector2.zero,playerData.wall) || playerData.CircleCheck(p2,0.1f,Vector2.zero,playerData.wall);

            if(isPlayerInHardWall)
                jumpCoroutine = jump.StartCoroutine(IEJump());
            else if(isPlayerInSoftWall){
                if(playerData.down)
                    IEDownJump();
                else
                    jumpCoroutine = jump.StartCoroutine(IEJump());
            }
        }

        public JumpHelper(PlayerData p,Jump j,JumpData jd){
            playerData = p;
            jump = j;
            jumpData = jd;

            scaleX = playerData.boxCollider2D.bounds.size.x / 2 - 0.1f;
            scaleY = playerData.boxCollider2D.bounds.size.y / 2;
        }
    }
}
