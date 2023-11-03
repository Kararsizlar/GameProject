using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player{

    public class PlayerInputR1 : InputHandler , IInputHandler{
        [Header("String değerinin doğru dünya ile eşleştiğinden emin ol.")]
        public string actionMapName;
        private Walk walk;
        private Jump jump;
        private LedgeGrab ledgeGrab;
        private Attack attack;
        private IPlayerMovementAction dash;
        private InputActionMap inputMap;
        private void Start(){
            SetUpActionHandler();
        }
        public void SetUpActionHandler(){
            inputMap = inputAsset.FindActionMap(actionMapName);
            walk = componentContainer.AddComponent<Walk>();
            jump = componentContainer.AddComponent<Jump>();
            dash = componentContainer.AddComponent<Dash>();
            ledgeGrab = componentContainer.AddComponent<LedgeGrab>();
            attack = componentContainer.AddComponent<Attack>();


            inputMap.Enable();
            inputMap.FindAction("Walk").started += walk.DoAction;
            inputMap.FindAction("Walk").canceled += walk.DoAction;
            inputMap.FindAction("Jump").started += jump.DoAction;
            inputMap.FindAction("Jump Cancel").started += jump.DoAction;
            inputMap.FindAction("Dash").started += dash.DoAction;
            inputMap.FindAction("Attack").started += attack.DoAction;
        }
    }
}