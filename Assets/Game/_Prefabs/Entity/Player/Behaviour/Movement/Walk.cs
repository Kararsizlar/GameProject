using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSpace{
    public class Walk : MonoBehaviour , IPlayerMovementAction
    {
        public WalkData walkData;
        [HideInInspector] public PlayerData playerData;
        private Coroutine Iact = null;
        private Coroutine Idrag = null;

        private void OnWalkStart(float newDirection){
            
            if(newDirection == 0)
                OnWalkEnd();

            if(Idrag != null){
                StopCoroutine(Idrag);
                Idrag = null;
            }

            if(Iact != null) 
                return;

            playerData.direction = (int)newDirection;
            playerData.onRun = true;

            if(playerData.onAir)
                playerData.animator.Play("Player_Fall");
            else
                playerData.animator.Play("Player_Run");

            Iact = StartCoroutine(IWalkContinue());
        }

        private void OnWalkContinue(){
            if(playerData.dashing)
                return;
            
            SetSpeed(walkData.speed * playerData.direction);
        }

        private void OnWalkEnd(){
            StopCoroutine(Iact);
            Iact = null;
            playerData.onRun = false;
            Idrag = StartCoroutine(IWalkDrag());
        }

        private void OnWalkDrag(){
            
            SetSpeed(playerData.playerBody2D.velocity.x * walkData.slowDownMultiplier);
            
            if(GetAbsoluteSpeed() < walkData.minSpeedBeforeStop){

                if(playerData.onAir)
                    playerData.animator.Play("Player_Fall");
                else
                    playerData.animator.Play("Player_Idle");

                SetSpeed(0);
            }
        }

        private void SetSpeed(float value){
            playerData.playerBody2D.velocity = new Vector2(value,playerData.playerBody2D.velocity.y);
        }

        private float GetAbsoluteSpeed(){
            return Mathf.Abs(playerData.playerBody2D.velocity.x);
        }

        private IEnumerator IWalkContinue(){
            while(true){
                yield return Time.fixedDeltaTime;
                OnWalkContinue();
            }
        }

        private IEnumerator IWalkDrag(){
            while(Iact == null && GetAbsoluteSpeed() != 0){;
                yield return Time.fixedDeltaTime;
                OnWalkDrag();
            }
        }

        public void DoAction(InputAction.CallbackContext callback){
            
            if(callback.started)
                OnWalkStart(callback.ReadValue<float>());
            
            if(callback.canceled)
                OnWalkEnd();
        }
    }
}