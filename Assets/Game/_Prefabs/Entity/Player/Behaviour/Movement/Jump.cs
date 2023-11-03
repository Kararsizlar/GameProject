using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player{
    public class Jump : MonoBehaviour , IPlayerMovementAction
    {

        private Transform player;
        private Rigidbody2D rigidBody;
        private Coroutine jumpCoroutine;
        private MovementData movementData;
        private JumpData jumpData;
        private float scaleX;
        private float scaleY;
        private float gap = 0.1f; //isim açıklayıcı değil

        private void Start(){
            player = transform.parent.parent;
            rigidBody = player.GetComponent<Rigidbody2D>();

            if(!TryGetComponent<MovementData>(out movementData))
                movementData = gameObject.AddComponent<MovementData>();

            jumpData = Resources.Load<JumpData>("MovementData/JumpData");
            scaleX = player.lossyScale.x / 2 - gap;
            scaleY = player.lossyScale.y / 2;
        }

        private void StopJump(){
            if(jumpCoroutine == null)
                return;
                
            StopCoroutine(jumpCoroutine);
            jumpCoroutine = null;
            rigidBody.velocity= new Vector2(rigidBody.velocity.x,0);
        }

        private IEnumerator IStartJump(){
            float startY = rigidBody.position.y;
            Vector2 leftPos  = new(rigidBody.position.x - scaleX,rigidBody.position.y + scaleY);
            Vector2 rightPos = new(rigidBody.position.x + scaleX,rigidBody.position.y + scaleY);

            while (rigidBody.position.y - startY < jumpData.maxDistance && !movementData.CircleCheck(rightPos) && !movementData.CircleCheck(leftPos)){
                leftPos  = new(rigidBody.position.x - scaleX,rigidBody.position.y + scaleY);
                rightPos = new(rigidBody.position.x + scaleX,rigidBody.position.y + scaleY);
                yield return Time.fixedDeltaTime;
                rigidBody.velocity = new(rigidBody.velocity.x,jumpData.speed);
            }
            StopJump();
        }

        public void DoAction(InputAction.CallbackContext callback){

            void StartJump(){
                Vector2 leftPos  = new(rigidBody.position.x - scaleX,rigidBody.position.y - scaleY);
                Vector2 rightPos = new(rigidBody.position.x + scaleX,rigidBody.position.y - scaleY);
                
                if (movementData.CircleCheck(leftPos) || movementData.CircleCheck(rightPos))
                    jumpCoroutine = StartCoroutine(IStartJump());
            }

            if(callback.action.name == "Jump")
                StartJump();
            else if(callback.action.name == "Jump Cancel")
                StopJump();
        }

        private void OnDrawGizmos(){
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(new(rigidBody.position.x - scaleX,rigidBody.position.y - scaleY), 0.1f);
            Gizmos.DrawSphere(new(rigidBody.position.x + scaleX,rigidBody.position.y - scaleY), 0.1f);
        }
    }
}
