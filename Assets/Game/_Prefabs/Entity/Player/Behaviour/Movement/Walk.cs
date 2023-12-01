using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSpace{
    public class Walk : MonoBehaviour
    {
        [SerializeField] PlayerData playerData;
        public WalkData walkData;
        private WalkHelper walkHelper;

        private void Start(){
            walkHelper = new(playerData,walkData,this);
        }

        public void DoAction(InputAction.CallbackContext callback){
            
            walkData.walkInput = (int)callback.ReadValue<float>();

            if(callback.started)
                walkHelper.StartWalk(callback.ReadValue<float>());
            
            if(callback.canceled)
                walkHelper.StopWalk();
        }
    }

    class WalkHelper{

        private Coroutine walkCoroutine;
        private PlayerData playerData;
        private WalkData walkData;
        private Walk walk;
        private IEnumerator IEWalk(float newDirection){
            
            if(newDirection == 0)
                yield break;

            playerData.direction = (int)newDirection;
            
            while(true){
                playerData.onRun = true;
                SetSpeed(walkData.speed * playerData.direction);
                yield return Time.fixedDeltaTime;
            }
        }

        private IEnumerator IEStop(){
            playerData.onRun = false;

            while (!playerData.onRun && GetAbsoluteSpeed() != 0){
                int sign = Math.Sign(GetAbsoluteSpeed());
                SetSpeed(playerData.playerBody2D.velocity.x * walkData.slowDownMultiplier);

                if(GetAbsoluteSpeed() < walkData.minSpeedBeforeStop)
                    SetSpeed(0);
                
                yield return Time.fixedDeltaTime;
            }
        }

        private void SetSpeed(float value){
            playerData.playerBody2D.velocity = new Vector2(value,playerData.playerBody2D.velocity.y);
        }

        private float GetAbsoluteSpeed(){
            return Mathf.Abs(playerData.playerBody2D.velocity.x);
        }

        public void StartWalk(float newDirection){
            playerData.onWalk = true;
            EventHub.walkStartedEvent?.Invoke();
            walkCoroutine = walk.StartCoroutine(IEWalk(newDirection));
        }

        public void StopWalk(){
            playerData.onWalk = false;
            EventHub.walkStoppedEvent?.Invoke();
            walk.StopCoroutine(walkCoroutine);

            if(!walkData.interrupted)
                walk.StartCoroutine(IEStop());
        }

        public void OnDashStart()
        {
            if(playerData.onWalk){
                walkData.interrupted = true;
                StopWalk();
            }
        }

        public void OnDashStop()
        {
            if(walkData.interrupted && walkData.walkInput != 0){
                playerData.onWalk = true;
                StartWalk(playerData.direction);
            }

            walkData.interrupted = false;
        }

        public WalkHelper(PlayerData p,WalkData wd,Walk w){
            playerData = p;
            walkData = wd;
            walk = w;

            EventHub.dashStartedEvent += OnDashStart;
            EventHub.dashStoppedEvent += OnDashStop;
        }
    }
}