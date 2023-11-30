using System.Collections;
using System.Collections.Generic;
using PlayerSpace;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSpace
{
    public class Dash : MonoBehaviour, IPlayerMovementAction
    {
        [SerializeField] PlayerData playerData;
        public DashData dashData;
        private DashHelper dashHelper;

        private void Start(){
            dashHelper = new(playerData,dashData,this);
        }

        public void DoAction(InputAction.CallbackContext context) {
            if(playerData.canDash == false || context.phase != InputActionPhase.Started)
                return;

            dashHelper.Dash();
        }
    }

    class DashHelper{
        private PlayerData playerData;
        private DashData dashData;
        private Dash dash;

        private IEnumerator IEStartDash(){
            DashTrailBehaviour.onDash.Invoke();
            playerData.canDash = false;
            playerData.dashing = true;
            playerData.playerBody2D.velocity = new(dashData.dashSpeed * playerData.direction,0);
            yield return new WaitForSeconds(dashData.dashActionTime);
            playerData.dashing = false;
            playerData.playerBody2D.velocity = new(0,playerData.playerBody2D.velocity.y);
            dash.StartCoroutine(IEWaitNextDash());
        }
        private IEnumerator IEWaitNextDash(){
            EventHub.dashStoppedEvent.Invoke();
            yield return new WaitForSeconds(dashData.timeBetweenDashes);
            playerData.canDash = true;
        }
        
        public void Dash(){
            dash.StartCoroutine(IEStartDash());
            EventHub.dashStartedEvent.Invoke();
        }
        public DashHelper(PlayerData p,DashData dd,Dash d){
            playerData = p;
            dashData = dd;
            dash = d;
        }
    }
}
