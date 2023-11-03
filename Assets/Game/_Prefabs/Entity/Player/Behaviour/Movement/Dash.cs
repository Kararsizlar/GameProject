using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour, IPlayerMovementAction
{
    private Transform player;
    private Rigidbody2D rigidBody;
    private MovementData movementData;
    private DashData dashData;

    private IEnumerator ISpotDodge(){
        movementData.canDash = false;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(dashData.dashActionTime);
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(DashTimer());

        yield return null;
    }
    private IEnumerator IDash(){
        movementData.canDash = false;
        movementData.dashing = true;
        rigidBody.velocity = new(dashData.dashSpeed * movementData.direction,0);
        yield return new WaitForSeconds(dashData.dashActionTime);
        movementData.dashing = false;

        StartCoroutine(DashTimer());
    }

    public void DoAction(InputAction.CallbackContext context) {
        Vector2 feetPos = (Vector2)player.position - new Vector2(0,player.lossyScale.y / 2); 
        
        if(movementData.canDash == false || context.phase != InputActionPhase.Started)
            return;
        
        if(movementData.CircleCheck(feetPos) && movementData.direction != 0)
            StartCoroutine(IDash());
        else
            StartCoroutine(ISpotDodge());
    }

    private IEnumerator DashTimer(){
        yield return new WaitForSeconds(dashData.timeBetweenDashes);
        movementData.canDash = true;
    }

    private void Start(){
        player = transform.parent.parent;
        rigidBody = player.GetComponent<Rigidbody2D>();
        dashData = Resources.Load<DashData>("MovementData/DashData");
        
        if(!TryGetComponent<MovementData>(out movementData))
            movementData = gameObject.AddComponent<MovementData>();
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
    }
}
