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
    private bool canDash = true;
    private Coroutine currentCoroutine;

    private IEnumerator ISpotDodge(){
        canDash = false;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(dashData.dashActionTime);
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(DashTimer());

        yield return null;
    }
    private IEnumerator IDash(){
        canDash = false;
        movementData.dashing = true;
        rigidBody.velocity = new(dashData.dashSpeed * movementData.direction,0);
        yield return new WaitForSeconds(dashData.dashActionTime);
        movementData.dashing = false;

        StartCoroutine(DashTimer());
    }

    public void DoAction(InputAction.CallbackContext context) {
        Vector2 feetPos = (Vector2)player.position - new Vector2(0,player.lossyScale.y / 2); 
        
        if(canDash == false || context.phase != InputActionPhase.Started)
            return;
        
        if(movementData.CastCheck(feetPos) && movementData.direction != 0)
            currentCoroutine = StartCoroutine(IDash());
        else
            currentCoroutine = StartCoroutine(ISpotDodge());
    }

    private IEnumerator DashTimer(){
        yield return new WaitForSeconds(dashData.timeBetweenDashes);
        canDash = true;
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
