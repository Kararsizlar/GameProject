using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    public class LedgeGrab : MonoBehaviour
    {

        [SerializeField] PlayerData playerData;
        [SerializeField] LedgeGrabData ledgeGrabData;
        private LedgeGrabHelper ledgeGrabHelper;
        private void Start(){
            ledgeGrabHelper = new(playerData,this,ledgeGrabData);
            ledgeGrabHelper.StartCheck();
        }
    }

    class LedgeGrabHelper{
        private Vector2 realScale,bodyPos,headPos,spacePos;
        private PlayerData playerData;
        private LedgeGrab ledgeGrab;
        private LedgeGrabData ledgeGrabData;
        
        private IEnumerator CheckLedge(){

            while(true){
                yield return Time.fixedDeltaTime;
                float directionReal = playerData.direction * (playerData.playerTransform.lossyScale.x + 0.1f) / 2;
                bodyPos  = playerData.playerBody2D.position + new Vector2(directionReal,0);
                headPos  = playerData.playerBody2D.position + new Vector2(directionReal,realScale.y / 2);
                spacePos = headPos + new Vector2(0,realScale.y);

                bool bodyCheck = playerData.CircleCheck(bodyPos,0.1f,Vector2.zero,playerData.wall);
                bool headCheck = playerData.CircleCheck(headPos,0.1f,Vector2.zero,playerData.wall);
                bool spaceCheck = playerData.CircleCheck(spacePos,0.1f,Vector2.zero,playerData.wall);
                
                if(bodyCheck && !headCheck && !spaceCheck){
                    StartLedgeGrab();
                    yield return new WaitForSeconds(ledgeGrabData.hangTime);
                    StopLedgeGrab();
                }

            }
        }

        private void StopLedgeGrab(){
            playerData.playerTransform.position = headPos + new Vector2(0,realScale.y / 2);
            playerData.playerBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            EventHub.grabStoppedEvent?.Invoke();
        }

        private void StartLedgeGrab(){
            playerData.playerTransform.position = new Vector3(playerData.playerTransform.position.x,bodyPos.y);
            playerData.playerBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            EventHub.grabStartedEvent?.Invoke();
        }

        public void StartCheck(){
            ledgeGrab.StartCoroutine(CheckLedge());
        }

        public LedgeGrabHelper(PlayerData p,LedgeGrab l,LedgeGrabData ld){
            playerData = p;
            ledgeGrab = l;
            ledgeGrabData = ld;
            realScale = playerData.boxCollider2D.bounds.size;
        }
    }
}
