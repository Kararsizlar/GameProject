using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    public class LedgeGrab : MonoBehaviour
    {

        [HideInInspector] public PlayerData playerData;
        public LedgeGrabData ledgeGrabData;
        private Vector2 realScale;
        private Vector2 bodyPos;
        private Vector2 headPos;
        private Vector2 spacePos;

        private IEnumerator CheckLedge(){

            while(true){
                yield return Time.fixedDeltaTime;
                float directionReal = playerData.direction * (playerData.playerTransform.lossyScale.x + 0.1f) / 2;
                bodyPos  = playerData.playerBody2D.position + new Vector2(directionReal,0);
                headPos  = playerData.playerBody2D.position + new Vector2(directionReal,realScale.y / 2);
                spacePos = headPos + new Vector2(0,realScale.y);

                bool bodyCheck = playerData.CircleCheck(bodyPos,0.1f,Vector2.zero,playerData.wall);
                bool headCheck = playerData.CircleCheck(headPos,0.1f,Vector2.zero,playerData.wall);
                bool spaceCheck  = playerData.CircleCheck(spacePos,0.1f,Vector2.zero,playerData.wall);

                if(bodyCheck && !headCheck && !spaceCheck){
                    
                    RaycastHit2D hit = Physics2D.Raycast(spacePos,Vector2.down,Mathf.Infinity,playerData.wall);
                    playerData.playerTransform.position = new Vector3(playerData.playerTransform.position.x,hit.point.y);

                    playerData.playerBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
                    yield return new WaitForSeconds(ledgeGrabData.hangTime);
                    playerData.playerTransform.position = headPos + new Vector2(0,realScale.y / 2);
                    playerData.playerBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }

        private void Start(){
            realScale = playerData.boxCollider2D.bounds.size;
            StartCoroutine(CheckLedge());
        }

        private void OnDrawGizmos(){

            if(!Application.isPlaying)
                return;
                
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(bodyPos,0.1f);
            Gizmos.DrawSphere(headPos,0.1f);
            Gizmos.DrawSphere(spacePos,0.1f);        
        }
    }
}
