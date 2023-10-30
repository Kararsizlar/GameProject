using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player{

    public class PlayerInputR1 : PlayerInputHandler{

        private Walk walk;
        private Jump jump;
        private LedgeGrab ledgeGrab;
        private IPlayerMovementAction dash;

        public override void SetUpActionHandler(){
            walk = componentContainer.AddComponent<Walk>();
            jump = componentContainer.AddComponent<Jump>();
            dash = componentContainer.AddComponent<Dash>();
            ledgeGrab = componentContainer.AddComponent<LedgeGrab>();


            inputMap.Enable();
            inputMap.FindAction("Walk").started += walk.DoAction;
            inputMap.FindAction("Walk").canceled += walk.DoAction;
            inputMap.FindAction("Jump").started += jump.DoAction;
            inputMap.FindAction("Jump Cancel").started += jump.DoAction;
            inputMap.FindAction("Dash").started += dash.DoAction;
        }
    }
}