using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player{
    public class Walk : MonoBehaviour , IPlayerMovementAction
    {
        private Transform player;
        private Rigidbody2D rigidBody;
        private MovementData movementData;
        private WalkData walkData;
        private Coroutine Iact = null;
        private Coroutine Idrag = null;

        private void OnWalkStart(float newDirection){
            
            movementData.direction = (int)newDirection;

            if(movementData.direction == 0)
                OnWalkEnd();

            if(Idrag != null){
                StopCoroutine(Idrag);
                Idrag = null;
            }

            if(Iact != null) 
                return;

            Iact = StartCoroutine(IWalkContinue());
        }

        private void OnWalkContinue(){
            if(movementData.dashing)
                return;
            
            SetSpeed(walkData.speed * movementData.direction);
        }

        private void OnWalkEnd(){
            movementData.direction = 0;
            StopCoroutine(Iact);
            Iact = null;

            Idrag = StartCoroutine(IWalkDrag());
        }

        private void OnWalkDrag(){
            
            SetSpeed(rigidBody.velocity.x * walkData.slowDownMultiplier);
            
            if(GetAbsoluteSpeed() < walkData.minSpeedBeforeStop)
                SetSpeed(0);
        }

        private void SetSpeed(float value){
            rigidBody.velocity = new Vector2(value,rigidBody.velocity.y);
        }

        private float GetAbsoluteSpeed(){
            return Mathf.Abs(rigidBody.velocity.x);
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

        private void Start(){
            walkData = Resources.Load<WalkData>("MovementData/WalkData");
            player = transform.parent.parent;
           
            if(!TryGetComponent<MovementData>(out movementData))
                movementData = gameObject.AddComponent<MovementData>();

            rigidBody = player.GetComponent<Rigidbody2D>();
        }
    }
}