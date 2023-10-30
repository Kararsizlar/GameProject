using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rigidBody;
    private Coroutine ledgeCoroutine;
    private MovementData movementData;
    private LedgeGrabData ledgeGrabData;
    private Vector2 realScale;
    private Vector2 bodyPos;
    private Vector2 headPos;
    private Vector2 spacePos;

    private IEnumerator CheckLedge(){

        while(true){
            yield return Time.fixedDeltaTime;
            float directionReal = movementData.direction * (player.transform.lossyScale.x + 0.1f) / 2;
            bodyPos  = rigidBody.position + new Vector2(directionReal,0);
            headPos  = rigidBody.position + new Vector2(directionReal,realScale.y / 2);
            spacePos = headPos + new Vector2(0,realScale.y);

            bool bodyCheck = movementData.CastCheck(bodyPos);
            bool headCheck = movementData.CastCheck(headPos);
            bool spaceCheck  = movementData.CastCheck(spacePos);

            if(bodyCheck && !headCheck && !spaceCheck){
                
                RaycastHit2D hit = Physics2D.Raycast(spacePos,Vector2.down,Mathf.Infinity,movementData.layerMask);
                player.transform.position = new Vector3(player.transform.position.x,hit.point.y);

                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                yield return new WaitForSeconds(ledgeGrabData.hangTime);
                player.transform.position = headPos + new Vector2(0,realScale.y / 2);
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }


    private void Start(){
        player = transform.parent.parent;
        rigidBody = player.GetComponent<Rigidbody2D>();
        ledgeGrabData = Resources.Load<LedgeGrabData>("MovementData/LedgeGrabData");
        realScale = player.transform.lossyScale;

        
        if(!TryGetComponent<MovementData>(out movementData))
            movementData = gameObject.AddComponent<MovementData>();

        ledgeCoroutine = StartCoroutine(CheckLedge());
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(bodyPos,0.1f);
        Gizmos.DrawSphere(headPos,0.1f);
        Gizmos.DrawSphere(spacePos,0.1f);        
    }
}
