using System.Collections;
using System.Collections.Generic;
using PlayerSpace;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSpace
{
    public class Dash : MonoBehaviour, IPlayerMovementAction
    {
        [HideInInspector] public PlayerData playerData;
        public DashData dashData;

        private IEnumerator ISpotDodge(){
            playerData.canDash = false;
            playerData.playerBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            yield return new WaitForSeconds(dashData.dashActionTime);
            playerData.playerBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            StartCoroutine(DashTimer());

            yield return null;
        }
        private IEnumerator IDash(){
            playerData.canDash = false;
            playerData.dashing = true;
            playerData.playerBody2D.velocity = new(dashData.dashSpeed * playerData.direction,0);
            yield return new WaitForSeconds(dashData.dashActionTime);
            playerData.dashing = false;
            playerData.playerBody2D.velocity = new(0,playerData.playerBody2D.velocity.y);
            StartCoroutine(DashTimer());
        }

        public void DoAction(InputAction.CallbackContext context) {
            Vector2 feetPos = (Vector2)playerData.boxCollider2D.bounds.center - new Vector2(0,playerData.boxCollider2D.bounds.size.y / 2); 
            
            if(playerData.canDash == false || context.phase != InputActionPhase.Started)
                return;
            
            if(playerData.CircleCheck(feetPos,0.1f,Vector2.zero,playerData.wall))
                StartCoroutine(IDash());
            else
                StartCoroutine(ISpotDodge());
        }

        private IEnumerator DashTimer(){
            yield return new WaitForSeconds(dashData.timeBetweenDashes);
            playerData.canDash = true;
        }
    }
}
